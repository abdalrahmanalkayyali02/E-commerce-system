using ECommerce.Application.Feature.userMangement.User.Create.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.UserMangement
{
    [ApiController]
  //  [ApiVersion("1.0")]
    [Route("register")]
    public class RegisterationController : BaseApiController
    {
        private readonly IMediator _mediator;

        public RegisterationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("customer")]
        public async Task<IActionResult> CustomerRegister([FromBody] CreateCustomerCommand request)
        {
            var result = await _mediator.Send(request);
            return HandleResult(result);
        }

        [HttpPost("seller")]
        public async Task<IActionResult> SellerRegister([FromBody] CreateSellerCommand request)
        {
            var result = await _mediator.Send(request);
            return HandleResult(result);
        }
    }
}