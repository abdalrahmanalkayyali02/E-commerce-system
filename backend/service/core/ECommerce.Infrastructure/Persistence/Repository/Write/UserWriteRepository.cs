using IAC.Domain.AggregateRoot;
using IAC.Domain.Repository.Write;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Write
{
    public class UserWriteRepository : IUserWriteRepository
    {
        public Task AddAsync(UserAggregate user, CancellationToken concellation = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid i, CancellationToken concellation = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserAggregate user, CancellationToken concellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
