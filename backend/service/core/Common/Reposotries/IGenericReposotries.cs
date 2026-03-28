using System.Linq.Expressions;

namespace Common.Reposotries
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default); 
        
        Task<T?> GetValueBySpecfication(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> selector, CancellationToken ct = default);

        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void SoftDelete(T entity); 

    }
}