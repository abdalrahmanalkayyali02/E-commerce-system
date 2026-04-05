using Common.Collection;
using Common.Enum;
using Common.Impl.Collection;
using Common.Impl.Result;
using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;
using MediatR;
using System.Linq.Expressions;

namespace ECommerce.Domain.modules.UserMangement.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user, CancellationToken cancellation = default);

        void Update(UserEntity user, CancellationToken cancellation = default);

        void SoftDelete(UserEntity user, CancellationToken cancellation = default);

        Task<UserEntity?> GetUserByID(Guid id, CancellationToken ct = default);
        Task<UserEntity?> GetUserByEmail(string email, CancellationToken ct = default);
        Task<UserEntity?> GetUserByPhoneNumber(string phoneNumber, CancellationToken ct = default);
        Task<UserEntity?> GetUserByUserName(string userName, CancellationToken ct = default);

        Task<UserEntity> GetUserByCriteria(Guid? id = null, string? userName = null,
            string? PhoneNumber = null, string? Email = null, CancellationToken ct = default);

        Task<PagedList<UserEntity>> GetUsersByCriteria(
            Guid? id = null,
            string? userName = null,
            string? phoneNumber = null,
            string? email = null,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken ct = default);
        Task<IEnumerable<UserEntity>> GetAllUsersAsync(Expression<Func<UserEntity, bool>> predicate = null, CancellationToken cancellationToken = default);
        Task<IPagedList<UserEntity>> GetUsersByFilterAsync(UserType? type, AccountStatus? accountStatus, bool? isDeleted, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}