using ECommerce.Domain.modules.Notification.Entity;
using MediatR;

public interface INotificationRepository
{
    public Task<NotificationEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
    public Task AddAsync(NotificationEntity notification, CancellationToken ct = default);
    public void Update(NotificationEntity notification, CancellationToken ct = default);
    public void Delete (NotificationEntity notification, CancellationToken ct = default);
    public Task<IEnumerable<NotificationEntity>> GetUnreadByUserIdAsync(Guid userId, CancellationToken ct = default);
    
    public Task<(IEnumerable<NotificationEntity> Items, int TotalCount)>
         GetPagedByUserIdAsync(Guid userId, int pageNum, int pageSize, CancellationToken ct);

    public Task<int> GetUnreadCountByUserIdAsync(Guid userId, CancellationToken ct);
    Task MarkAllAsReadByUserIdAsync(Guid userID, CancellationToken ct);
}