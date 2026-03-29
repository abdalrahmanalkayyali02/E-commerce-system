using Common.DTOs.Notification;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Notification.usersNotification.GetById.Queries
{
    public record GetNotificationByIdQuery(Guid id, Guid userID) : IRequest<Result<NotificationDetailDto>>;
    
    
}
