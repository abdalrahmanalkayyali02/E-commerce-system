using Common.Enum;
using ECommerce.Application.Abstraction.IExternalService;

namespace ECommerce.Infrastructure.ExternalService
{
    public class FirebaseCloudMessagingNotificationgatway : INotificationGateway
    {
        public Task BroadcastAsync(string message, string subject = null)
        {
            throw new NotImplementedException();
        }

        public Task SendToMultipleUsersAsync(IEnumerable<Guid> userIds, string message, string subject = null)
        {
            throw new NotImplementedException();
        }

        public Task SendToRoleAsync(UserRole roleName, string message, string subject = null)
        {
            throw new NotImplementedException();
        }

        public Task SendToUserAsync(Guid userId, string message, string subject = null)
        {
            throw new NotImplementedException();
        }
    }
}
