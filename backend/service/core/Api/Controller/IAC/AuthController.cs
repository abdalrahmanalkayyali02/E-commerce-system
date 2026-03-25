using ECommerce.Application.Feature.IAC.User.GetProfile.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Api.Controller.IAC
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuthController (IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the profile details based on ID and Role.
        /// Once JWT is implemented, we will extract the ID and Role from the Token.
        /// </summary>
        [HttpGet("me")]
        public async Task<IActionResult> GetProfile([FromQuery] GetUserByIdQueries query)
        {
            // MediatR will automatically map the ?id= and ?role= from the URL
            var result = await _mediator.Send(query);

            return HandleResult(result);
        }
    }
}
