using IAC.Domain.AggregateRoot;
using IAC.Domain.Repository.Write;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Write
{
    public class SellerWriteRepository : ISellerWriteRepository
    {
        public Task AddAsync(SellerAggregate seller, CancellationToken concellation = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsunc(SellerAggregate seller, CancellationToken concellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
