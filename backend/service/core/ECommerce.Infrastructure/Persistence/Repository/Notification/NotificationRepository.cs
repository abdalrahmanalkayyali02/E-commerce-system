using ECommerce.Domain.modules.Notification.Entity;
using ECommerce.Infrastructure.Persistence.Mapper.Notification;
using ECommerce.Infrastructure.Persistence.Model.Notification;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repository.Notification
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(NotificationEntity notification, CancellationToken ct = default)
        {
            var model = NotificationMapper.ToPersistence(notification);
            await _context.Notifications.AddAsync(model, ct);
        }

        public async Task<NotificationEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var model = await _context.Notifications
                .AsNoTracking() 
                .FirstOrDefaultAsync(n => n.notificationID == id && !n.IsDeleted, ct);

            return NotificationMapper.ToEntity(model);
        }

        public async Task<(IEnumerable<NotificationEntity> Items, int TotalCount)> 
            GetPagedByUserIdAsync(Guid userId, int pageNum, int pageSize, CancellationToken ct)
        {
            var query = _context.Notifications
                .Where(n => n.ReceiverId == userId && !n.IsDeleted)
                .OrderByDescending(n => n.CreatedAt);

            int totalCount = await query.CountAsync(ct);

            var models = await query
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var entities = models.Select(NotificationMapper.ToEntity);

            return (entities, totalCount);
        }

        public async Task<IEnumerable<NotificationEntity>> GetUnreadByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            var models = await _context.Notifications
                .Where(n => n.ReceiverId == userId && !n.IsRead && !n.IsDeleted)
                .ToListAsync(ct);

            return models.Select(NotificationMapper.ToEntity);
        }

        public async Task<int> GetUnreadCountByUserIdAsync(Guid userId, CancellationToken ct)
        {
            return await _context.Notifications
                .CountAsync(n => n.ReceiverId == userId && !n.IsRead && !n.IsDeleted, ct);
        }

        public void Update(NotificationEntity notification, CancellationToken ct = default)
        {
            var model = NotificationMapper.ToPersistence(notification);
            _context.Notifications.Update(model);
        }

        public void Delete(NotificationEntity notification, CancellationToken ct = default)
        {
            var model = NotificationMapper.ToPersistence(notification);
            _context.Notifications.Update(model);
        }

        public async Task MarkAllAsReadByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            await _context.Notifications
                .Where(n => n.ReceiverId == userId && !n.IsRead && !n.IsDeleted)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(n => n.IsRead, _ => true)
                    .SetProperty(n => n.ReadAt, _ => DateTime.UtcNow)
                    .SetProperty(n => n.UpdatedAt, _ => DateTime.UtcNow),
                ct);
        }
    }
}