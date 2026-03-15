using IAC.Domain.Repository.Write;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Write
{
    public class UserOTPWriteRepository : IWriteUserOTpRepository
    {
        public Task SetOtpForUser(Guid userId, string otp, CancellationToken concellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
