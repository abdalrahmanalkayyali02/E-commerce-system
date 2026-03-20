using ECommerce.Domain.modules.IAC.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.Repositories
{
    public interface ISellerRepository
    {

        public Task AddAsync(SellerEntity entity, CancellationToken cancellation = default);
        public void Update(SellerEntity entity, CancellationToken cancellation = default);

        public IEnumerable<Task<SellerEntity>> GetAllSellerNotVerfiedSellerDocument(CancellationToken cancellationToken = default);
        public IEnumerable<Task<SellerEntity>> GetAllSellerNotVerfiedShopDocument(CancellationToken cancellationToken = default);
        public IEnumerable<Task<SellerEntity>> GetAllSellerNotVerfiedByAdmin(CancellationToken cancellationToken = default);
        public IEnumerable<Task<SellerEntity>> GetAllSellerThatVerifedByAdmin(CancellationToken cancellationToken = default);
    }
}
