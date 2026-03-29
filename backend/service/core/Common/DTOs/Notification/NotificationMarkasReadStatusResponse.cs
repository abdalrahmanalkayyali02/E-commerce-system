using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.Notification
{
    public record NotificationMarkasReadStatusResponse(string message, Guid notificationID, bool isRead);
    
    
}
