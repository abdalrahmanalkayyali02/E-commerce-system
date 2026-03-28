using System;
using Common.Enum;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.UserMangement.DomainError;
using ECommerce.Domain.modules.UserMangement.ValueObject;

namespace ECommerce.Domain.modules.UserMangement.Entity
{
    public class UserOTPEntity
    {
        // --- Properties ---
        public Guid ID { get; init; }
        public Guid userID { get; init; }
        public OTP Code { get; init; }
        public bool IsUsed { get; private set; }
        public bool IsVerified { get; private set; }
        public int FailedAttempts { get; private set; }
        public OtpType OTPtype { get; init; }
        public DateTime GeneratedAt { get; init; }
        public DateTime ExpiresAt { get; init; }
        public DateTime UpdateAt { get; private set; } = DateTime.UtcNow;
        public DateTime? TimeVerfied { get; private set; }

        // --- Constructors ---
        internal UserOTPEntity() { }

        public UserOTPEntity(
            Guid id,
            Guid userId,
            OTP code,
            OtpType type,
            DateTime generatedAt,
            DateTime expiresAt,
            DateTime updateAt,
            DateTime? timeVerfied,
            bool isUsed,
            bool isVerified,
            int failedAttempts)
        {
            ID = id;
            userID = userId;
            Code = code;
            OTPtype = type;
            GeneratedAt = generatedAt;
            ExpiresAt = expiresAt;
            UpdateAt = updateAt;
            TimeVerfied = timeVerfied;
            IsUsed = isUsed;
            IsVerified = isVerified;
            FailedAttempts = failedAttempts;
        }

        // --- Factory Method ---
        public static UserOTPEntity Create(
            Guid id,
            Guid userID,
            OTP otp,
            OtpType type,
            int expiryMinutes = 10)
        {
            var now = DateTime.UtcNow;
            return new UserOTPEntity(
                id, userID, otp, type,
                now, now.AddMinutes(expiryMinutes),
                now, null, false, false, 0);
        }

        // --- Domain Logic Methods ---

        public bool IsValid() =>
            !IsUsed &&
            DateTime.UtcNow <= ExpiresAt &&
            FailedAttempts < 5;

        public bool IsExpired() => DateTime.UtcNow >= ExpiresAt;

        public Result<bool> MarkAsVerified()
        {
            if (!IsValid())
                return Result<bool>.Failure(OtpDomainErrors.Expired);

            IsVerified = true;
            TimeVerfied = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
            return Result<bool>.Success(true);
        }

        public void MarkAsUsed()
        {
            IsUsed = true;
            UpdateAt = DateTime.UtcNow;
        }

        public void MarkAsVerfied()
        {
            IsUsed = true;
            IsVerified = true;
            TimeVerfied = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void IncrementFailedAttempts()
        {
            FailedAttempts++;
            UpdateAt = DateTime.UtcNow;
        }
    }
}