using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Model;
using WebApplication1.Repository.Interface;

namespace WebApplication1.Repository.Impl;

public class SellerRepository : ISellerRepository
{
    private readonly AppDbContext _context;

    public SellerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SellerDataModel entity, CancellationToken cancellation = default)
    {
        if (entity.CreateAt == default)
            entity.CreateAt = DateTime.UtcNow;

        await _context.Sellers.AddAsync(entity, cancellation);
    }

    public void Update(SellerDataModel entity, CancellationToken cancellation = default)
    {
        entity.UpdateAt = DateTime.UtcNow;
        _context.Sellers.Update(entity);
    }

    public async Task<SellerDataModel?> GetSellerWithId(Guid id, CancellationToken cancellation = default)
    {
        return await _context.Sellers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SellerId == id, cancellation);
    }

    public async Task<IEnumerable<SellerDataModel>> GetAllSellerNotVerfiedSellerDocument(CancellationToken cancellationToken = default)
    {
        return await _context.Sellers
            .AsNoTracking()
            .Where(x => !x.isVerfiedSellerDocumentBeenViewed)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<SellerDataModel>> GetAllSellerNotVerfiedShopDocument(CancellationToken cancellationToken = default)
    {
        return await _context.Sellers
            .AsNoTracking()
            .Where(x => !x.isVerfiedShopDocumentBeenViewed)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<SellerDataModel>> GetAllSellerNotVerfiedByAdmin(CancellationToken cancellationToken = default)
    {
        return await _context.Sellers
            .AsNoTracking()
            .Where(x => !x.isVerifiedByAdmin)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<SellerDataModel>> GetAllSellerThatVerifedByAdmin(CancellationToken cancellationToken = default)
    {
        return await _context.Sellers
            .AsNoTracking()
            .Where(x => x.isVerifiedByAdmin)
            .ToListAsync(cancellationToken);
    }
}