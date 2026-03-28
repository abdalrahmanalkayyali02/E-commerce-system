using Common.Specfication;
using ECommerce.Domain.modules.Notification.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Domain.modules.Notification.Resposotires
{
    public interface INotificationRepository
    {
        Task<NotificationEntity> GetByIdAsync(Guid id);
        Task<IReadOnlyList<NotificationEntity>> ListAllAsync();

        Task<IReadOnlyList<NotificationEntity>> ListAsync(ISpecification<NotificationEntity> spec);

        Task AddAsync(NotificationEntity entity);
        void Update(NotificationEntity entity);
        void Delete(NotificationEntity entity);
    }
}