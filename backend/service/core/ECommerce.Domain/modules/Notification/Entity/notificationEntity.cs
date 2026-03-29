using Common.Enum;
using ECommerce.Domain.modules.Notification.ValueObject;
using ECommerce.Domain.modules.Notification.ValueObject.ECommerce.Domain.modules.Notification.ValueObjects;
using System;

namespace ECommerce.Domain.modules.Notification.Entity
{
    public class NotificationEntity
    {
        public Guid notificationID { get; private set; }
        public Guid ReceiverId { get; private set; }
        public NotificationTitle Title { get; private set; }
        public NotificationBody Body { get; private set; }
        public NotificationType Type { get; private set; }
        public NotificationPriority Priority { get; private set; }

        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; } 
        public DateTime? DeletedAt { get; private set; } 
        public bool IsDeleted { get; private set; } 

        // Private constructor
        private NotificationEntity(Guid notificationID,Guid receiverId, NotificationTitle title, NotificationBody body, NotificationType type)
        {
            this.notificationID = notificationID;
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

        private NotificationEntity() { }



        // Factory Method
        public static NotificationEntity Create(Guid notificationID,Guid receiverId, NotificationTitle title, NotificationBody body, NotificationType type)
        {
            return new NotificationEntity(notificationID,receiverId, title, body, type);
        }

        public static NotificationEntity CreateFromPersistence(
            Guid id, Guid receiverId, NotificationTitle title, NotificationBody body,
            NotificationType type, NotificationPriority priority,
            bool isRead, DateTime? readAt, DateTime createdAt,
            DateTime updatedAt, bool isDeleted, DateTime? deletedAt)
        {
            return new NotificationEntity 
            {
                notificationID = id,
                ReceiverId = receiverId,
                Title = title,
                Body = body,
                Type = type,
                Priority = priority,
                IsRead = isRead,
                ReadAt = readAt,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt,
                IsDeleted = isDeleted,
                DeletedAt = deletedAt
            };
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