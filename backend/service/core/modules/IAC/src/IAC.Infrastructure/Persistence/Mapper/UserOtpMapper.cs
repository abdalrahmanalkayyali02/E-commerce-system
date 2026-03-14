using IAC.Domain.Entity;
using IAC.Infrastructure.Persistence.Model;
using IAC.Domain.Value_Object; 

namespace IAC.Infrastructure.Persistence.Mapper
{
    public static class UserOtpMapper
    {
        // from domain => to model 
        public static UserOtpModel ToPersistence( UserOTP userOtp)
        {

            if (userOtp == null) return null;

            return new UserOtpModel
            {
                Id = userOtp.ID,
                Code = userOtp.Code.Value, 
                IsUsed = userOtp.IsUsed,
                IsVerified = userOtp.IsVerified,
                FailedAttempts = userOtp.FailedAttempts,
                UserId = userOtp.userID,
                GeneratedAt = userOtp.GeneratedAt,
                ExpiresAt = userOtp.ExpiresAt
            };
        }

        // from Model => to Domain
        public static UserOTP ToDomain( UserOtpModel model)
        {
            if (model == null) return null;

            return UserOTP.Load(
                model.Id,
                model.UserId,
                OTP.From(model.Code), 
                model.GeneratedAt,
                model.ExpiresAt,
                model.IsUsed,
                model.IsVerified,
                model.FailedAttempts
            );
        }
    }
}