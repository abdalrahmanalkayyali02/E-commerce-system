using Api.ViewModels.web.Adminstration.Request;
using Common.Collection;
using Common.Result;
using Common.DTOs.Adminstration;
using Common.DTOs.UserMangement.User;
using Common.Enum;
using ECommerce.Application.Feature.Adminstration.ManegeUser.DeleteUserByCiteria.Command;
using ECommerce.Application.Feature.Adminstration.ManegeUser.GetAllUserByFilterQueryForUserType.Query;
using ECommerce.Application.Feature.Adminstration.ManegeUser.LockedUserByCiteria.Command;
using ECommerce.Application.Feature.Adminstration.ManegeUser.UnLockedUserByCiteria.Query;
using ECommerce.Application.Feature.Notification.usersNotification.GetById.Queries;
using ECommerce.Application.Feature.userMangement.User.Profile.GetProfile.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.Controller.Adminstration
{
    [ApiController]
    [Route("admin/users")]
    [Authorize]
    // role policy is admin
    public class UserAdminstrationController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UserAdminstrationController(IMediator mediater)
        {
            _mediator = mediater;
        }

        [HttpDelete("delete")]
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

        [HttpPatch("unlocked")]
        public async Task<ActionResult<UserDto>> UnlockedUser([FromQuery] AdminUserActionDTOs command)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var adminID))
            {
                return Unauthorized("User ID not found in token.");
            }

            var commands = new UnlockUserByCriteriaQuery(
                adminID,
                command.Id,
                command.UserName,
                command.PhoneNumber,
                command.Email
            );

            // 3. Send to Mediator
            var result = await _mediator.Send(commands);
            return (ActionResult)HandleResult(result);
        }


        [HttpGet]
        public async Task<ActionResult<IPagedList<UserDto>>> GetAllUser([FromQuery] GetAllUsersByFilterRequest queries)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var adminID))
            {
                return Unauthorized("User ID not found in token.");
            }

            var query = new GetAllUsersByFilterQuery(adminID, queries.PageNumber, queries.PageSize, queries.UserType);

            var result = await _mediator.Send(query);
            return (ActionResult)HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetUserDetails(Guid id)
        {
            var query = new GetUserByIdQueries(id);
            var result = await _mediator.Send(query);

            return (ActionResult)HandleResult(result);
        }





    }
}
