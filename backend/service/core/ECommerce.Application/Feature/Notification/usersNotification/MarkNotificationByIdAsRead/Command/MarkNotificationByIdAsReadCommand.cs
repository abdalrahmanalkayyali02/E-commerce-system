using Common.DTOs.Notification;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Notification.usersNotification.MarkNotificationByIdAsRead.Command
{
    public record MarkNotificationByIdAsReadCommand(Guid receiverID, Guid id, bool isRead) : IRequest<Result<NotificationMarkasReadStatusResponse>>;



}
