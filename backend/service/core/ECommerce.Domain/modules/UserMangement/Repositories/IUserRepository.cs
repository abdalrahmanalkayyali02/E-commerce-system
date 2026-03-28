using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;
using System.Linq.Expressions;

namespace ECommerce.Domain.modules.UserMangement.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user, CancellationToken cancellation = default);

        void Update(UserEntity user, CancellationToken cancellation = default);

        void SoftDelete(UserEntity user, CancellationToken cancellation = default);

        Task<UserEntity?> GetEntityWithSpec(ISpecification<UserEntity> spec, CancellationToken cancellationToken = default);
        Task<UserEntity?> GetUserByID(Guid id, CancellationToken ct = default);
        Task<UserEntity?> GetUserByEmail(string email, CancellationToken ct = default);
        Task<UserEntity?> GetUserByPhoneNumber(string phoneNumber, CancellationToken ct = default);
        Task<UserEntity?> GetUserByUserName(string userName, CancellationToken ct = default);


        Task<IEnumerable<UserEntity>> GetAllUsersAsync(Expression<Func<UserEntity, bool>> predicate = null, CancellationToken cancellationToken = default);
    }
}