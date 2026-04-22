using System.Net;
using Microsoft.AspNetCore.Mvc;
using Tastemap.Core.Common;
using Tastemap.Core.DTOs.Auth;
using Tastemap.Core.Services.AuthServices;

namespace TasteMap.API.Controllers.AuthControllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : CustomControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _authService.RegisterAsync(registerDto);
        return CreateActionResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        return CreateActionResult(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] string refreshToken)
    {
        var result = await _authService.LogoutAsync(refreshToken);
        return CreateActionResult(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        var result = await _authService.RefreshTokenAsync(refreshToken);
        return CreateActionResult(result);
    }
}