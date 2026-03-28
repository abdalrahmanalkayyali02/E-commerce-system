using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAM.Entity
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }

        public bool IsUsed { get; set; }     
        public bool IsRevoked { get; set; }   

        public Guid UserId { get; set; }
        public Guid DeviceId { get; set; }
        public string DeviceInfo { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsActive => !IsRevoked && !IsUsed && !IsExpired;

        public void markAsRevoked()
        {
            IsRevoked = true;
        }


        
    }
}
