using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.modules.IAC.Repositories;
using ECommerce.Infrastructure.Persistence.Mapper.IAC;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.Repository.IAC
{
    public class SellerRepository : ISellerRepository
    {
        private readonly AppDbContext _context;

        public SellerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SellerEntity entity, CancellationToken cancellation = default)
        {
            var model = SellerMapper.FromDomainToPersistence(entity);
            await _context.Sellers.AddAsync(model, cancellation);
        }

        public void Update(SellerEntity entity, CancellationToken cancellation = default)
        {
            var model = SellerMapper.FromDomainToPersistence(entity);
            _context.Sellers.Update(model);
        }

        public async Task<IEnumerable<SellerEntity>> GetAllSellerThatVerifedByAdmin(CancellationToken cancellationToken = default)
        {
            var sellers = await _context.Sellers
                .AsNoTracking()
                .Where(s => s.isVerifiedByAdmin)
                .ToListAsync(cancellationToken);

            return sellers.Select(SellerMapper.FromPersistenceToDomain);
        }

        public async Task<IEnumerable<SellerEntity>> GetAllSellerNotVerfiedByAdmin(CancellationToken cancellationToken = default)
        {
            var sellers = await _context.Sellers
                .AsNoTracking()
                .Where(s => !s.isVerifiedByAdmin)
                .ToListAsync(cancellationToken);

            return sellers.Select(SellerMapper.FromPersistenceToDomain);
        }

        public async Task<IEnumerable<SellerEntity>> GetAllSellerNotVerfiedSellerDocument(CancellationToken cancellationToken = default)
        {
            var sellers = await _context.Sellers
                .AsNoTracking()
                .Where(s => !s.isVerfiedSellerDocumentBeenViewed)
                .ToListAsync(cancellationToken);

            return sellers.Select(SellerMapper.FromPersistenceToDomain);
        }

        public async Task<IEnumerable<SellerEntity>> GetAllSellerNotVerfiedShopDocument(CancellationToken cancellationToken = default)
        {
            var sellers = await _context.Sellers
                .AsNoTracking()
                .Where(s => !s.isVerfiedShopDocumentBeenViewed)
                .ToListAsync(cancellationToken);

            return sellers.Select(SellerMapper.FromPersistenceToDomain);
        }
    }
}