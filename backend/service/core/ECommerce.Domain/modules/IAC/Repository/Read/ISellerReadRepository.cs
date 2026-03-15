using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Read
{
    public interface ISellerReadRepository
    {
        public List<Task<SellerAggregate>> GetAllSeller(CancellationToken cancellationToken = default);
        public List<Task<SellerAggregate>> GetAllSellerNotVerfiedSellerDocument(CancellationToken cancellationToken = default);
        public List<Task<SellerAggregate>> GetAllSellerNotVerfiedShopDocument(CancellationToken cancellationToken = default);

        public List<Task<SellerAggregate>> GetAllSellerNotVerfiedByAdmin(CancellationToken cancellationToken = default);
        public List<Task<SellerAggregate>> GetAllSellerThatVerifedByAdmin(CancellationToken cancellationToken = default);
    }
}
