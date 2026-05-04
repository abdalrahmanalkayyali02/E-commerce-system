using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Shared.Collection.Impl;

public class PagedList<T> : IPagedList<T>
{
    public IReadOnlyList<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public int PageSize { get; }

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items.AsReadOnly();
        TotalCount = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    // Static factory method to simplify repository usage
    public static async Task<PagedList<T>> CreateAsync(
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