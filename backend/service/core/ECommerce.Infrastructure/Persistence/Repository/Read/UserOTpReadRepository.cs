using IAC.Domain.Entity;
using IAC.Domain.Repository.Read;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Read
{
    internal class UserOTpReadRepository : IUserOTpReadRepository
    {
        public Task<IEnumerable<UserOTPEntity>> GetAll(CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<UserOTPEntity> GetById(Guid id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetOtpByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
