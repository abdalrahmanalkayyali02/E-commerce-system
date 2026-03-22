using ECommerce.Domain.modules.IAC.Entity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Domain.modules.IAC.Repositories
{
    public interface ISellerRepository
    {
        Task AddAsync(SellerEntity entity, CancellationToken cancellation = default);

        void Update(SellerEntity entity, CancellationToken cancellation = default);

        Task<IEnumerable<SellerEntity>> GetAllSellerNotVerfiedSellerDocument(CancellationToken cancellationToken = default);

        Task<IEnumerable<SellerEntity>> GetAllSellerNotVerfiedShopDocument(CancellationToken cancellationToken = default);

        Task<IEnumerable<SellerEntity>> GetAllSellerNotVerfiedByAdmin(CancellationToken cancellationToken = default);

        Task<IEnumerable<SellerEntity>> GetAllSellerThatVerifedByAdmin(CancellationToken cancellationToken = default);
    }
}