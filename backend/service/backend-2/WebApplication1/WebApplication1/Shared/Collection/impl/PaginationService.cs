using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Shared.Collection.Impl;

public class PaginationService : IPaginationService
{
    public async Task<IPagedList<T>> CreateAsync<T>(
        IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default)
    {
        var count = await source.CountAsync(ct);

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}