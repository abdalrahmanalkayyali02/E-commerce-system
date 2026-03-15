using IAC.Domain.Enum;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstraction
{
    public interface INotificationGateway
    {
        Task SendToUserAsync(Guid userId, string message, string subject = null);

        Task SendToRoleAsync(UserRole roleName, string message, string subject = null);

        Task BroadcastAsync(string message, string subject = null);

        Task SendToMultipleUsersAsync(IEnumerable<Guid> userIds, string message, string subject = null);
    }
}