using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Domain.modules.UserMangement.Repositories
{
    public interface ISellerRepository
    {
        Task AddAsync(SellerEntity entity, CancellationToken cancellation = default);

        void Update(SellerEntity entity, CancellationToken cancellation = default);
        public Task<SellerEntity?> GetEntityWithSpec(ISpecification<SellerEntity> spec, CancellationToken cancellationToken = default);
        public Task<SellerEntity?> GetSellerWithID(Guid id, CancellationToken cancellation = default);


        Task<IEnumerable<SellerEntity>> GetAllSellerNotVerfiedSellerDocument(CancellationToken cancellationToken = default);

        Task<IEnumerable<SellerEntity>> GetAllSellerNotVerfiedShopDocument(CancellationToken cancellationToken = default);

        Task<IEnumerable<SellerEntity>> GetAllSellerNotVerfiedByAdmin(CancellationToken cancellationToken = default);

        Task<IEnumerable<SellerEntity>> GetAllSellerThatVerifedByAdmin(CancellationToken cancellationToken = default);
    }
}