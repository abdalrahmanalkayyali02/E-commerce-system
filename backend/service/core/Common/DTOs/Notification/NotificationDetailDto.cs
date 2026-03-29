using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.Notification
{
    public record NotificationDetailDto(
        Guid Id,
        Guid receiverID,
        string Title,
        string Body,
        NotificationType Type, 
        NotificationPriority Priority, 
        bool IsRead,
        DateTime CreatedAt,
        DateTime? ReadAt
    );
}
