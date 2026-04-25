using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tastemap.Core.Common;
using Tastemap.Core.DTOs.Auth;
using Tastemap.Core.Entities.Authentication;
using Tastemap.Core.Entities.UserEntities;
using Tastemap.Core.Enums;
using Tastemap.Core.Repositories;
using Tastemap.Core.Services.AuthServices;
using Tastemap.Core.Settings;

namespace Tastemap.Service.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;

    public AuthService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings)
    {
        _unitOfWork = unitOfWork;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Response<TokenResponseDto>> RegisterAsync(RegisterDto registerDto)
    {
        if (await _unitOfWork.Users.ExistByEmailAsync(registerDto.Email))
            return Response<TokenResponseDto>.Fail("Email already registered");
        if (await _unitOfWork.Users.ExistByUsernameAsync(registerDto.Username))
            return Response<TokenResponseDto>.Fail("Username already registered");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = registerDto.Email,
            Username = registerDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            Status = UserStatus.Active,
            CreatedAt = DateTime.UtcNow.AddHours(4),
            UpdatedAt = DateTime.UtcNow.AddHours(4)
        };

        var userDetails = new UserDetails()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            PhoneNumber = registerDto.PhoneNumber,
            ProfileImageUrl = null,
            User = user,
            UserId = user.Id
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.UserDetails.AddAsync(userDetails);
        await _unitOfWork.SaveChangesAsync();

        var tokens = await GenerateTokenAsync(user);
        return Response<TokenResponseDto>.Success(tokens);
    }

    public async Task<Response<TokenResponseDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(loginDto.Email);
        if (user is null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            return Response<TokenResponseDto>.Fail("Email or Password incorrect");
        if (user.Status != UserStatus.Active)
            return Response<TokenResponseDto>.Fail("User is not active");
        var token = await GenerateTokenAsync(user);
        return Response<TokenResponseDto>.Success(token);
    }

    public async Task<Response<TokenResponseDto>> RefreshTokenAsync(string refreshToken)
    {
        var token = await _unitOfWork.RefreshTokens.GetByRefreshToken(refreshToken);
        if (token is null)
            return Response<TokenResponseDto>.Fail("Refresh token not found");
        if (token.ExpiresAt < DateTime.UtcNow.AddHours(4))
            return Response<TokenResponseDto>.Fail("Refresh token is expired");

        await _unitOfWork.RefreshTokens.RevoveAsync(token);
        await _unitOfWork.SaveChangesAsync();

        var tokens = await GenerateTokenAsync(token.User);
        return Response<TokenResponseDto>.Success(tokens);
    }

    public async Task<Response<bool>> LogoutAsync(string refreshToken)
    {
        var token = await _unitOfWork.RefreshTokens.GetByRefreshToken(refreshToken);

        if (token is null)
            return Response<bool>.Fail("Refresh token not found");

        await _unitOfWork.RefreshTokens.RevoveAsync(token);
        await _unitOfWork.SaveChangesAsync();
        return Response<bool>.Success(true);
    }


    //Private methods

    private async Task<TokenResponseDto> GenerateTokenAsync(User user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = await GenerateRefreshTokenAsync(user.Id);

        return new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = DateTime.UtcNow.AddHours(4).AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
        };
    }

    private string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Username)
        };

        foreach (var userRole in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4).AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId)
    {
        var refreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddHours(4).AddDays(_jwtSettings.RefreshTokenExpirationDays),
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow.AddHours(4)
        };

        await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync();
        return refreshToken;
    }
}