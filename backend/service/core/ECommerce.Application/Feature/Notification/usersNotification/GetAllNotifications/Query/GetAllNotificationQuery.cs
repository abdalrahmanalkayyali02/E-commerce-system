using Common.DTOs.Notification;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Notification.usersNotification.GetAllNotifications.Query
{
    public record  GetAllNotificationQuery (Guid userId, int pageNumber, int pageSize)
        : IRequest<Result<IEnumerable<NotificationDetailDto>>>;
    
    
}
