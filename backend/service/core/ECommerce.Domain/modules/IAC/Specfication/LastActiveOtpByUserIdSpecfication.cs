using Common.Enum;
using Common.Specfication;
using ECommerce.Domain.modules.IAC.Entity;

public class LastActiveOtpByUserIdSpecfication : BaseSpecification<UserOTPEntity>
{
    // Remove the 'string otp' parameter here
    public LastActiveOtpByUserIdSpecfication(Guid userID, OtpType type)
        : base(u => u.userID == userID &&
                    u.IsUsed == false &&
                    u.OTPtype == type)
    {
        AddOrderByDescending(u => u.GeneratedAt);
        ApplyPaging(0, 1);
    }
}