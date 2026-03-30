using Common.DTOs.Adminstration;
using ECommerce.Application.Feature.Adminstration.ManegeUser.DeleteUserByCiteria.Command;
using ECommerce.Application.Feature.Adminstration.ManegeUser.LockedUserByCiteria.Command;
using ECommerce.Application.Feature.Notification.usersNotification.GetById.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.Controller.Adminstration
{
    [ApiController]
    [Route("admin/user")]
    [Authorize]
    // role policy is admin
    public class UserAdminstrationController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UserAdminstrationController(IMediator mediater)
        {
            _mediator = mediater;
        }

        [HttpPatch("delete")]
        public async Task<IActionResult> DeleteUser([FromQuery] AdminUserActionDTOs command)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var adminID))
            {
                return Unauthorized("User ID not found in token.");
            }

            var commands = new DeleteUserByCriteriaCommand(adminID,command.Id,command.UserName,command.PhoneNumber,command.Email);

            var result = await _mediator.Send(commands);

            return HandleResult(result);
        }


        [HttpPatch("locked")]
        public async Task<IActionResult> LockedUser([FromQuery] AdminUserActionDTOs command)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var adminID))
            {
                return Unauthorized("User ID not found in token.");
            }

            var commands = new LockUserByCriteriaCommand(
                adminID,
                command.Id,
                command.UserName,
                command.PhoneNumber,
                command.Email
            );

            // 3. Send to Mediator
            var result = await _mediator.Send(commands);
            return HandleResult(result);
        }

    }
}
