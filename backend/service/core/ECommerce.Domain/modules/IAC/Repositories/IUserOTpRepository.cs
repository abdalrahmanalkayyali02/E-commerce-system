using ECommerce.Domain.modules.IAC.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.Repositories
{
    public interface IUserOTpRepository
    {
        public Task AddAsync(UserOTPEntity userOTPEntity,CancellationToken cancellationToken = default);
        public void Update (UserOTPEntity userOTPEntity, CancellationToken cancellationToken = default);

        public void SoftDelete (UserOTPEntity userOTPEntity,CancellationToken cancellation = default);

        public Task DeleteAsync(Guid id);

        
    }
}
