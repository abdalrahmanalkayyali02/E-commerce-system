using Common.Enum;
using Common.Specfication;
using ECommerce.Domain.modules.IAC.Entity;
using System;

namespace ECommerce.Domain.modules.IAC.Specfication
{
    public class LastOtpVerifiedWithinWindowSpecification : BaseSpecification<UserOTPEntity>
    {
        public LastOtpVerifiedWithinWindowSpecification(Guid userID)
            : base(u => u.userID == userID &&
                        u.OTPtype == OtpType.forgotPassword &&
                        u.IsUsed == true && // Must be used/verified
                        u.UpdateAt >= DateTime.UtcNow.AddMinutes(-10)) // Window check
        {
            // Get the most recently updated one
            AddOrderByDescending(u => u.UpdateAt);
            ApplyPaging(0, 1);
        }
    }
}