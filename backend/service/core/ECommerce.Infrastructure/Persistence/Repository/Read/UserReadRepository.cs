using IAC.Domain.AggregateRoot;
using IAC.Domain.Repository.Read;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Read
{
    public class UserReadRepository : IUserReadRepository
    {
        public Task<IEnumerable<UserAggregate>> GetAll(CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<UserAggregate> GetById(Guid id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task<UserAggregate?> GetUserByEmail(string email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UserAggregate?> GetUserByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UserAggregate?> GetUserByUsername(string username, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
