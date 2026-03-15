using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Read
{
    public interface IUserOTpReadRepository
    {
        public Task<string> GetOtpByUserId(Guid userId, CancellationToken cancellationToken =default);
    }
}
