using IAC.Domain.AggregateRoot;
using IAC.Domain.Repository.Write;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Write
{
    public class CustomerWriteRepository : ICustomerWriteRepository
    {
        public Task AddAsync(CustomerAggregate item, CancellationToken cancellation = default)
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

        public void Update(CustomerAggregate item, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
