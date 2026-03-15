using IAC.Domain.Repository.Read;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Read
{
    internal class UserOTpReadRepository : IUserOTpReadRepository
    {
        public Task<string> GetOtpByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
