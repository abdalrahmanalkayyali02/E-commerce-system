using ECommerce.Application.Feature.Notification.usersNotification.DeleteById.Command;
using ECommerce.Application.Feature.Notification.usersNotification.GetAllNotifications.Query;
using ECommerce.Application.Feature.Notification.usersNotification.GetById.Queries;
using ECommerce.Application.Feature.Notification.usersNotification.GetUnreadCountNotification.Query;
using ECommerce.Application.Feature.Notification.usersNotification.MarkAllNotificationAsRead.Command;
using ECommerce.Application.Feature.Notification.usersNotification.MarkNotificationByIdAsRead.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.Controller.Notification
{
    [ApiController]
    [Route("/notification")]
    [Authorize]
    public class notificationsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public notificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetNotificationByID([FromRoute] Guid id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var query = new GetNotificationByIdQuery(id, userId);

            var result = await _mediator.Send(query);

            return HandleResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteNotificationByID([FromRoute] Guid id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var command = new DeleteNotificationByIdCommand(userId, id);
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }


        [HttpPatch("{id:guid}/read")]
        public async Task<IActionResult> UpdateNotficationReadStatus([FromRoute] Guid id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var command = new MarkNotificationByIdAsReadCommand(userId, id,true);
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPatch("mark-all-read")]
        public async Task<IActionResult> UpdateAllNotificationAsRead()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var command = new MarkAllNotificationAsReadCommand(userId);
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized("User ID not found in token.");

            var query = new GetAllNotificationQuery(userId, pageNumber, pageSize);

            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadNotificationCount()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var query = new GetUnreadCountQuery(userId);

            var result = await _mediator.Send(query);

            return HandleResult(result);
        }






    }
}
