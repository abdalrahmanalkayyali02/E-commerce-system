using Common.Enum;
using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.UserMangement.Repositories
{
    public interface IUserOTpRepository
    {
        public Task AddAsync(UserOTPEntity userOTPEntity,CancellationToken cancellationToken = default);
        public void Update (UserOTPEntity userOTPEntity, CancellationToken cancellationToken = default);

        public void SoftDelete (UserOTPEntity userOTPEntity,CancellationToken cancellation = default);

        public Task<UserOTPEntity?> GetLastOtpOfType(Guid userId, OtpType type, CancellationToken cancellation = default);





    }
}
