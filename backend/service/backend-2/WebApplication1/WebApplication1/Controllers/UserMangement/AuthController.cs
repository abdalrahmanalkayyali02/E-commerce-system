using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Service.Interface;
using WebApplication1.Shared;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Enum;
using WebApplication1.Shared.Result;

namespace WebApplication1.Controllers.UserMangement;

[ApiController]
[Route("auth")]
public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    
    public async Task<IActionResult> Login([FromBody] LoginDTos request, CancellationToken ct)
    {
        var result = await _authService.UserLogin(request, ct);
        return HandleResult(result);
    }
    
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetProfile(CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRoleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userRoleClaim))
        {
            return Unauthorized(Error.Failure("Auth.InvalidToken", "Missing required claims in token."));
        }

        if (!Guid.TryParse(userIdClaim, out var userId) ||
            !Enum.TryParse<UserType>(userRoleClaim, out var userType))
        {
            return BadRequest(Error.Validation("Auth.InvalidData", "Token claims format is invalid."));
        }

        var result = await _authService.GetMe(userId, ct);

        return HandleResult(result);
    }
}