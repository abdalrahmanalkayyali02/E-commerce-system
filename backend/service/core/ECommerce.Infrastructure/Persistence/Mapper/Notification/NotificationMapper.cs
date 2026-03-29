using ECommerce.Domain.modules.Notification.Entity;
using ECommerce.Domain.modules.Notification.ValueObject;
using ECommerce.Domain.modules.Notification.ValueObject.ECommerce.Domain.modules.Notification.ValueObjects;
using ECommerce.Infrastructure.Persistence.Model.Notification;

namespace ECommerce.Infrastructure.Persistence.Mapper.Notification
{
    public static class NotificationMapper
    {
        // 1. From Database Model to Domain Entity (Reconstruction)
        public static NotificationEntity ToEntity(notificationsModel model)
        {
            if (model == null) return null;

            // Use the Reconstructors to bypass validation logic when loading from DB
            var title = NotificationTitle.Reconstructor(model.Title);
            var body = NotificationBody.Reconstrcter(model.Body);

            return NotificationEntity.CreateFromPersistence(
                model.notificationID,
                model.ReceiverId,
                title,
                body,
                model.Type,
                model.Priority,
                model.IsRead,
                model.ReadAt,
                model.CreatedAt,
                model.UpdatedAt,
                model.IsDeleted,
                model.DeletedAt
            );
        }

        public static notificationsModel ToPersistence(NotificationEntity entity)
        {
            if (entity == null) return null;

            return new notificationsModel
            {
                notificationID = entity.notificationID,
                ReceiverId = entity.ReceiverId,
                Title = entity.Title.Value,
                Body = entity.Body.Value,
                Type = entity.Type,
                Priority = entity.Priority,
                IsRead = entity.IsRead,
                ReadAt = entity.ReadAt,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsDeleted = entity.IsDeleted,
                DeletedAt = entity.DeletedAt
            };
        }
    }
}