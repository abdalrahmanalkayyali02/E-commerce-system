using Common.Collection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Abstractions
{
    public interface IPaginationService
    {
        Task<IPagedList<T>> CreateAsync<T>(
            IQueryable<T> source,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default);
    }
}