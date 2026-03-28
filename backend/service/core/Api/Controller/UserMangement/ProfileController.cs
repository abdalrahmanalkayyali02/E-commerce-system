using Common.Enum;
using Common.Result;
using ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controller.UserMangement
{
    [ApiController]
    [Route("/profile")]
    [Authorize]
    public class ProfileController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("user")]
        public async Task<IActionResult> UpdateBaseProfile([FromBody] userProfileCommand command)
        {
            return HandleResult(await _mediator.Send(command with { userID = GetUserId() }));
        }

        [HttpPatch("seller")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> UpdateSellerProfile([FromBody] sellerProfileCommand command)
        {
            return HandleResult(await _mediator.Send(command with { userID = GetUserId() }));
        }

        [HttpPatch("customer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerProfile([FromBody] customerProfileCommand command)
        {
            return HandleResult(await _mediator.Send(command with { userID = GetUserId() }));
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
        }
    }
}