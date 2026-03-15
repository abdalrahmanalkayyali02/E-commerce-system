using Common.Abstraction.Reposotries;
using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Read
{
    public interface ISellerReadRepository : IReadReposotries<SellerAggregate>
    {
        public IEnumerable<Task<SellerAggregate>> GetAllSellerNotVerfiedSellerDocument(CancellationToken cancellationToken = default);
        public IEnumerable<Task<SellerAggregate>> GetAllSellerNotVerfiedShopDocument(CancellationToken cancellationToken = default);
        public IEnumerable<Task<SellerAggregate>> GetAllSellerNotVerfiedByAdmin(CancellationToken cancellationToken = default);
        public IEnumerable<Task<SellerAggregate>> GetAllSellerThatVerifedByAdmin(CancellationToken cancellationToken = default);
    }
}
