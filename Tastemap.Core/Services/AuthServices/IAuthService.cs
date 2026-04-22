using Tastemap.Core.Common;
using Tastemap.Core.DTOs.Auth;

namespace Tastemap.Core.Services.AuthServices;

public interface IAuthService
{
    Task<Response<TokenResponseDto>> RegisterAsync(RegisterDto registerDto);
    Task<Response<TokenResponseDto>> LoginAsync(LoginDto loginDto);
    Task<Response<TokenResponseDto>> RefreshTokenAsync(string refreshToken);
    Task<Response<bool>> LogoutAsync(string refreshToken);
}