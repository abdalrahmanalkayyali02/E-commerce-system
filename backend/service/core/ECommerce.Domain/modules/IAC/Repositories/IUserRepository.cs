using Common.Specfication;
using ECommerce.Domain.modules.IAC.Entity;
using System.Linq.Expressions;

namespace ECommerce.Domain.modules.IAC.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user, CancellationToken cancellation = default);

        void Update(UserEntity user, CancellationToken cancellation = default);

        void SoftDelete(UserEntity user, CancellationToken cancellation = default);

        Task DeleteAsync(Guid id);

        Task<UserEntity?> GetEntityWithSpec(ISpecification<UserEntity> spec, CancellationToken cancellationToken = default);

        Task<IEnumerable<UserEntity>> GetAllUsersAsync(Expression<Func<UserEntity, bool>> predicate = null, CancellationToken cancellationToken = default);
    }
}