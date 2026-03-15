using Common.Abstraction.Reposotries;
using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Write
{
    public interface ICustomerWriteRepository : IWriteReposotries<CustomerAggregate>
    {
    }
}
