using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Write
{
    public interface ISellerWriteRepository
    {
        public Task AddAsync(SellerAggregate seller, CancellationToken concellation = default);
        public Task UpdateAsunc(SellerAggregate seller, CancellationToken concellation = default);

    }
}
