using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Read
{
    public interface IUserOTpRepository
    {
        public Task<string> GetOtpByUserId(Guid userId);
    }
}
