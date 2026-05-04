using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Service.Interface.UserMangement;
using WebApplication1.Shared;
using WebApplication1.Shared.Enum;

namespace WebApplication1.Controllers.UserMangement;

[ApiController]
[Route("identity/verify")]
public class VerificationController : BaseApiController
{
    private readonly IVerificationService _verificationService;

    public VerificationController(IVerificationService verificationService)
    {
        _verificationService = verificationService;
    }
    
    
    [HttpPost("{type:otpType}")]
    public async Task<IActionResult> Verify([FromRoute] OtpType type, [FromBody] VerfiedOtpDtOsRequest request,CancellationToken ct)
    {
        var result = await _verificationService.VerifiedOtpService(request.Email, request.Otp, type, ct);

        return HandleResult(result);
    }

    [HttpPost("resend/{type:otpType}")]
    public async Task<IActionResult> Resend(
        [FromRoute] OtpType type,
        [FromBody] ResendOtpDtOsRequest request,CancellationToken ct)
    {
        var result = await _verificationService.ResendOtpService(request.Email, type, ct);

        return HandleResult(result);
    }
}