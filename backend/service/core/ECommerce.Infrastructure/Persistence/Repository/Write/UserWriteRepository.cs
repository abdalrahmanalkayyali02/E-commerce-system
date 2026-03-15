using IAC.Domain.AggregateRoot;
using IAC.Domain.Repository.Write;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Write
{
    public class UserWriteRepository : IUserWriteRepository
    {
        public Task AddAsync(UserAggregate item, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(Guid id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public void Update(UserAggregate item, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
