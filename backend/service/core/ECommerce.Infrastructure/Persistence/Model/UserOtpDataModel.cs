using IAC.Domain.Value_Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Model
{
    public class UserOtpDataModel
    {
        public Guid ID { get;  set; }
        public Guid userID { get;  set; }
        public OTP Code { get;  set; }
        public DateTime GeneratedAt { get;  set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public bool IsVerified { get; set; }
        public int FailedAttempts { get; set; }
    }
}
