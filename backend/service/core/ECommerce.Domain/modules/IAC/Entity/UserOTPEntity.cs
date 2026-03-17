using ECommerce.Domain.modules.IAC.ValueObject;

namespace ECommerce.Domain.modules.IAC.Entity
{
    public class UserOTPEntity
    {
        public Guid ID { get;init; }
        public Guid userID { get; init; }
        public OTP Code { get; init; }
        public DateTime GeneratedAt { get; init; }
        public DateTime ExpiresAt { get; init; }
        public bool IsUsed { get; internal set; }
        public bool IsVerified { get; internal set; }
        public int FailedAttempts { get;internal set; }

        internal UserOTPEntity() { }

        public static UserOTPEntity Create(Guid id, Guid userID, OTP otp, int expiryMinutes = 10)
        {
            return new UserOTPEntity
            {
                ID = id,
                userID = userID,
                Code = otp,
                GeneratedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes),
                IsUsed = false,
                IsVerified = false,
                FailedAttempts = 0
            };
        } 

        public UserOTPEntity(Guid iD, Guid userID, OTP code, DateTime generatedAt, DateTime expiresAt, bool isUsed, bool isVerified, int failedAttempts)
        {
            ID = iD;
            this.userID = userID;
            Code = code;
            GeneratedAt = generatedAt;
            ExpiresAt = expiresAt;
            IsUsed = isUsed;
            IsVerified = isVerified;
            FailedAttempts = failedAttempts;
        }

        public bool IsValid() =>
            !IsUsed &&
            DateTime.UtcNow <= ExpiresAt &&
            FailedAttempts < 5;

        public void MarkAsVerified()
        {
            if (!IsValid()) throw new InvalidOperationException("Cannot verify an expired or used OTP.");
            IsVerified = true;
        }

        public void MarkAsUsed() => IsUsed = true;

        public void IncrementFailedAttempts() => FailedAttempts++;
    }
}