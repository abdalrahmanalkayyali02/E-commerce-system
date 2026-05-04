using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Service.Interface.UserMangement;
using WebApplication1.Shared;

namespace WebApplication1.Controllers.UserMangement;

[ApiController]
[Route("profile")]
[Authorize] // Ensure all profile actions require a valid token
public class ProfileController : BaseApiController
{
    private readonly IProfileService _profileService;
    
    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }
    
    [HttpPatch("user")]
    public async Task<IActionResult> UpdateBaseProfile([FromBody] UpdateUserProfileDtOs command, CancellationToken ct)
    {
        var updatedCommand = command with { UserId = GetUserId() };
        
        var result = await _profileService.UpdateUserProfile(updatedCommand, ct);
        return HandleResult(result);
    }

    [HttpPatch("seller")]
    [Authorize(Roles = "Seller")]
    public async Task<IActionResult> UpdateSellerProfile([FromForm] UpdateSellerProfileDtOs command, CancellationToken ct)
    {
        var updatedCommand = command with { UserId = GetUserId() };
        
        var result = await _profileService.UpdateSellerProfile(updatedCommand, ct);
        return HandleResult(result);
    }

    [HttpPatch("customer")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> UpdateCustomerProfile([FromForm] UpdateCustomerProfileDtOs command, CancellationToken ct)
    {
        var updatedCommand = command with { UserId = GetUserId() };
        
        var result = await _profileService.UpdateCustomerProfile(updatedCommand, ct);
        return HandleResult(result);
    }

    private Guid GetUserId()
    {
        // Using NameIdentifier to match your JWT generation logic
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
    }
}