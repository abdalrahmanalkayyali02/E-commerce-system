using IAC.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Domain.Repository.Read
{
    public interface ISellerReadRepository
    {
        public List<Task<SellerAggregate>> GetAllSeller();
        public List<Task<SellerAggregate>> GetAllSellerNotVerfiedSellerDocument();
        public List<Task<SellerAggregate>> GetAllSellerNotVerfiedShopDocument();

        public List<Task<SellerAggregate>> GetAllSellerNotVerfiedByAdmin();
        public List<Task<SellerAggregate>> GetAllSellerThatVerifedByAdmin();
    }
}
