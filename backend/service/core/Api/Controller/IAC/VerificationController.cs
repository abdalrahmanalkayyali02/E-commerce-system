using Common.Enum;
using ECommerce.Application.Feature.IAC.OtpVerification.Verified.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Threading.Tasks;

namespace Api.Controller.IAC
{
    [ApiController]
    [Route("identity/verify")]
    public class VerificationController : BaseApiController
    {
        private readonly IMediator _mediator;

        public VerificationController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }


        [HttpPost("{type:otpType}")]
        public async Task<IActionResult> Verify([FromRoute] OtpType type, [FromBody] VerfiedOtpCommand command)
        {
            var result = await _mediator.Send(command with { type = type });

            return HandleResult(result);
        }

        [HttpPost("resend/{type:otpType}")]
        public async Task<IActionResult> Resend(
                    [FromRoute] OtpType type,
                    [FromBody] ResendOtpCommand command)
        {
            // Ensuring the command carries the correct OtpType from the URL
            var result = await _mediator.Send(command with { type = type });

            return HandleResult(result);
        }
    }
}