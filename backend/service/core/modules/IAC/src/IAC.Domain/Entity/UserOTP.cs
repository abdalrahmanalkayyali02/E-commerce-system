using IAC.Domain.Value_Object;

namespace IAC.Domain.Entity
{
    public class UserOTP
    {
        public Guid ID { get; private set; }
        public Guid userID { get; private set; }
        public OTP Code { get; private set; }
        public DateTime GeneratedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public bool IsUsed { get; private set; }
        public bool IsVerified { get; private set; }
        public int FailedAttempts { get; private set; }

        private UserOTP() { }

        public static UserOTP Create(Guid id, Guid userID, OTP otp, int expiryMinutes = 10)
        {
            return new UserOTP
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

        public static UserOTP Load(
            Guid id,
            Guid userId,
            OTP code,
            DateTime generatedAt,
            DateTime expiresAt,
            bool isUsed,
            bool isVerified,
            int failedAttempts)
        {
            return new UserOTP
            {
                ID = id,
                userID = userId,
                Code = code,
                GeneratedAt = generatedAt,
                ExpiresAt = expiresAt,
                IsUsed = isUsed,
                IsVerified = isVerified,
                FailedAttempts = failedAttempts
            };
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