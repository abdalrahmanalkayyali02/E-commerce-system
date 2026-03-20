using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.modules.IAC.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository
{
    public class SellerRepository : ISellerRepository
    {
        public Task AddAsync(SellerEntity entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task<SellerEntity>> GetAllSellerNotVerfiedByAdmin(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task<SellerEntity>> GetAllSellerNotVerfiedSellerDocument(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task<SellerEntity>> GetAllSellerNotVerfiedShopDocument(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task<SellerEntity>> GetAllSellerThatVerifedByAdmin(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(SellerEntity entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
