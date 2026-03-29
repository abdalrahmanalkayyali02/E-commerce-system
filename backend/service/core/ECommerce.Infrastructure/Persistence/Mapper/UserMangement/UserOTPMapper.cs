using ECommerce.Domain.modules.UserMangement.Entity;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using ECommerce.Infrastructure.Persistence.Model.UserMangement;

namespace ECommerce.Infrastructure.Persistence.Mapper.UserMangement
{
    public static class UserOTPMapper
    {
        public static UserOtpDataModel FromDomainToPersistence(UserOTPEntity domain)
        {
            return new UserOtpDataModel
            {
                ID = domain.ID,
                userID = domain.userID,
                Code = domain.Code.Value,
                GeneratedAt = domain.GeneratedAt,
                ExpiresAt = domain.ExpiresAt,
                IsUsed = domain.IsUsed,
                IsVerified = domain.IsVerified,
                FailedAttempts = domain.FailedAttempts,
                OTPtype = domain.OTPtype,
                UpdateAt = domain.UpdateAt,
                TimeVerfied = domain.TimeVerfied
                
            };
        }

        // Persistence => Domain
        public static UserOTPEntity FromPersistenceToDomain(UserOtpDataModel model)
        {
            return new UserOTPEntity(
                id: model.ID,
                userId: model.userID,
                code: OTP.Reconstructing(model.Code),
                type: model.OTPtype,
                generatedAt: model.GeneratedAt,
                expiresAt: model.ExpiresAt,
                updateAt: model.UpdateAt,
                timeVerfied: model.TimeVerfied,
                isUsed: model.IsUsed,
                isVerified: model.IsVerified,
                failedAttempts: model.FailedAttempts
            );
        }
    }
}