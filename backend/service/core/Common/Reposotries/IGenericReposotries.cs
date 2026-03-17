using System.Linq.Expressions;

namespace Common.Reposotries
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);

        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

        // WRITES
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void SoftDelete(T entity); // Better to pass the entity than just the ID
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    }
}