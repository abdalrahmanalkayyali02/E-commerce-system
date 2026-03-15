using ECommerce.Application.DTO.IAC.User.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.IAC
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/Register")]
    public class RegisterationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RegisterationController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost("/customer")]
        public async Task<IActionResult> CustomerRegister([FromBody] CreateCustomerRequest request)
        {

            var result = _mediator.Send(request);

            if (result == null)
                return  BadRequest("Registration failed.");

            return Ok(result);
        }

        [HttpPost("/seller")]
        public async Task<IActionResult> SellerRegister([FromBody] CreateSellerRequest request)
        {
            var result = _mediator.Send(request);
            if (result == null)
                return BadRequest("Registration failed.");
            return Ok(result);
        }
        
        
    }
}
