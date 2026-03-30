using Common.Enum;
using Common.Result;
using ECommerce.Application.Feature.userMangement.User.Login.Command;
using ECommerce.Application.Feature.userMangement.User.Profile.GetProfile.Queries;
using ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controller.UserMangement
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
        [Authorize]
        public async Task<IActionResult> GetProfile()
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

            var query = new GetUserByIdQueries(userId);

            var result = await _mediator.Send(query);

            return HandleResult(result);
        }



 

        [HttpPost("login")]
        public async Task <IActionResult> Login([FromBody] LoginUserCommand request)
        {
            var result = await _mediator.Send(request);
            return HandleResult(result);
        }
    }
}
