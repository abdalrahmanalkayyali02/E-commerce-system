using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Write
{
    public interface ISellerWriteRepository
    {
        public Task AddAsync(SellerAggregate seller);
        public Task UpdateAsunc(SellerAggregate seller);

    }
}
