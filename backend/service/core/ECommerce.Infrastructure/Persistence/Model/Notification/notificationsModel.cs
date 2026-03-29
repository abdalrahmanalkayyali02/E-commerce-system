using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Model.Notification
{
    public class notificationsModel
    {
        public Guid notificationID { get;  set; }
        public Guid ReceiverId { get;  set; }
        public string Title { get;  set; }
        public string Body { get;  set; }
        public NotificationType Type { get;  set; }
        public NotificationPriority Priority { get;  set; }

        public bool IsRead { get;  set; }
        public DateTime? ReadAt { get;  set; }

        public DateTime CreatedAt { get;  set; }
        public DateTime UpdatedAt { get;  set; }
        public DateTime? DeletedAt { get;  set; }
        public bool IsDeleted { get;  set; }
    }
}
