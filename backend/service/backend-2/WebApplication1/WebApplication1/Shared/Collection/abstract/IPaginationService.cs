namespace WebApplication1.Shared.Collection;
    public interface IPaginationService
    {
        Task<IPagedList<T>> CreateAsync<T>(
            IQueryable<T> source,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default);
    }