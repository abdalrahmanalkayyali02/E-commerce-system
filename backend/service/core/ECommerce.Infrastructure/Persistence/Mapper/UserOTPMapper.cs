
using ECommerce.Infrastructure.Persistence.Model;
using IAC.Domain.Entity;

namespace ECommerce.Infrastructure.Persistence.Mapper
{
    public static class UserOTPMapper
    {
        // Domain => Persistence 

        public static UserOtpDataModel FromDomainToPersistence(UserOTPEntity domain)
        {
            var model = new UserOtpDataModel
            {
                ID = domain.ID,
                userID = domain.userID,
                Code = domain.Code,
                GeneratedAt = domain.GeneratedAt,
                ExpiresAt = domain.ExpiresAt,
                IsUsed = domain.IsUsed,
                IsVerified = domain.IsVerified,
                FailedAttempts = domain.FailedAttempts
            };

            return model;
        }


        // Persistence => Domain

        public static UserOTPEntity FromPersistenceToDomain(UserOtpDataModel model)
        {
            var domain = new UserOTPEntity (
                iD: model.ID,
                userID: model.userID,
                code: model.Code,
                generatedAt: model.GeneratedAt,
                expiresAt: model.ExpiresAt,
                isUsed: model.IsUsed,
                isVerified: model.IsVerified,
                failedAttempts: model.FailedAttempts
                );
            return domain;
        }
    }
}