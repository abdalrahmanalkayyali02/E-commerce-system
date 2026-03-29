using Common.DTOs.Notification;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Notification.usersNotification.DeleteById.Command
{
    public record DeleteNotificationByIdCommand(Guid reciverId, Guid notificationID) : IRequest<Result<NotificationDeleteResponse>>;
    
    
}
