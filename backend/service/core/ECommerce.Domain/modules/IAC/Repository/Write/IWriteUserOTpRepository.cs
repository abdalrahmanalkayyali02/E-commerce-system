using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Write
{
    public interface IWriteUserOTpRepository
    {
        public Task SetOtpForUser(Guid userId, string otp, CancellationToken concellation = default);
        
    }
}
