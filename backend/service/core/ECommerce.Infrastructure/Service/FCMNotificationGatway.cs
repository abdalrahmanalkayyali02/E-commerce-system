using ECommerce.Application.Abstraction;
using IAC.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ECommerce.Infrastructure.Service
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
