using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;
using ECommerce.Domain.modules.UserMangement.Repositories;
using ECommerce.Infrastructure.Persistence.Mapper.UserMangement;
using ECommerce.Infrastructure.Persistence.Model.UserMangement;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.Repository.UserMangement
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

        public async Task<SellerEntity?> GetEntityWithSpec(ISpecification<SellerEntity> spec, CancellationToken cancellationToken = default)
        {
            IQueryable<SellerDataModel> query = _context.Sellers.AsNoTracking();

            var evaluatedQuery = query.Select(m => SellerMapper.FromPersistenceToDomain(m));

            var finalQuery = SpecificationEvaluator<SellerEntity>.GetQuery(evaluatedQuery, spec);

            return await finalQuery.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<SellerEntity?> GetSellerWithID(Guid id, CancellationToken cancellation = default)
        {
            var model = await _context.Sellers
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.sellerID == id,cancellation);

            return model != null ? SellerMapper.FromPersistenceToDomain(model) : null;
        }
    }
}