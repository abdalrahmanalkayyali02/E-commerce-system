using System;
using Common.Enum;

namespace ECommerce.Infrastructure.Persistence.Model.IAC
{
    public class UserOtpDataModel
    {
        public Guid ID { get; set; }
        public Guid userID { get; set; }
        public string Code { get; set; }
        public bool IsUsed { get; set; }
        public bool IsVerified { get; set; }
        public int FailedAttempts { get; set; }
        public OtpType OTPtype { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime? TimeVerfied { get; set; }
    }
}