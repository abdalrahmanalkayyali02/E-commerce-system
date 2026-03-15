using IAC.Domain.AggregateRoot;
using IAC.Domain.Repository.Write;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Write
{
    public class SellerWriteRepository : ISellerWriteRepository
    {
        public Task AddAsync(SellerAggregate item, CancellationToken cancellation = default)
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

        public void Update(SellerAggregate item, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
