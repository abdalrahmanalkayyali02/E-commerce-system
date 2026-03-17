using ECommerce.Domain.modules.IAC.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.Repositories
{
    public interface IUserRepository
    {
        public Task<UserEntity?> GetUserByEmail(string email, CancellationToken cancellationToken = default);
        public Task<UserEntity?> GetUserByUsername(string username, CancellationToken cancellationToken = default);
        public Task<UserEntity?> GetUserByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default);
    }
}
