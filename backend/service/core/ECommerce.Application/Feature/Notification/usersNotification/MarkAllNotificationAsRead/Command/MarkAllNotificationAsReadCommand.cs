using Common.DTOs.Notification;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Notification.usersNotification.MarkAllNotificationAsRead.Command
{
    public record MarkAllNotificationAsReadCommand(Guid userID) : IRequest<Result<MarksAllNotificatonAsReadStatusResponse>>;
    
    
}
