using Common.Enum;
using System;

namespace ECommerce.Domain.modules.Notification.Entity
{
    public class NotificationEntity
    {
        public Guid ReceiverId { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public NotificationType Type { get; private set; }
        public NotificationPriority Priority { get; private set; }

        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; } 
        public DateTime? DeletedAt { get; private set; } 
        public bool IsDeleted { get; private set; } 

        // Private constructor
        private NotificationEntity(Guid receiverId, string title, string body, NotificationType type)
        {
            ReceiverId = receiverId;
            Title = title;
            Body = body;
            Type = type;
            IsRead = false;
            IsDeleted = false;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Priority = NotificationPriority.Normal;
        }

        // Factory Method
        public static NotificationEntity Create(Guid receiverId, string title, string body, NotificationType type)
        {
            return new NotificationEntity(receiverId, title, body, type);
        }

        public void MarkAsRead()
        {
            if (!IsRead)
            {
                IsRead = true;
                ReadAt = DateTime.UtcNow;
                UpdatedAt = DateTime.UtcNow; 
            }
        }

        public void MarkAsDeleted()
        {
            if (!IsDeleted)
            {
                IsDeleted = true;
                DeletedAt = DateTime.UtcNow;
                UpdatedAt = DateTime.UtcNow; 
            }
        }
    }
}