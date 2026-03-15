using IAC.Domain.AggregateRoot;
using IAC.Domain.Repository.Write;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Write
{
    public class CustomerWriteRepository : ICustomerWriteRepository
    {
        public Task AddAsync(CustomerAggregate customer, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CustomerAggregate customer, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
