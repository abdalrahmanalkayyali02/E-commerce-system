using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Write
{
    public interface ICustomerWriteRepository
    {
        Task AddAsync(CustomerAggregate customer);
        Task UpdateAsync(CustomerAggregate customer);
        
    }
}
