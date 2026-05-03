using System.Linq.Expressions;
using WebApplication1.Data.Model;
using WebApplication1.Shared.Collection;
using WebApplication1.Shared.Collection.Impl;
using WebApplication1.Shared.Enum;

namespace WebApplication1.Repository.Interface;
 public interface IUserRepository
 {
     Task AddAsync(UserDataModel user, CancellationToken cancellation = default);

     void Update(UserDataModel user, CancellationToken cancellation = default);

     void SoftDelete(UserDataModel user, CancellationToken cancellation = default);

     Task<UserDataModel?> GetUserByID(Guid id, CancellationToken ct = default);
     Task<UserDataModel?> GetUserByEmail(string email, CancellationToken ct = default);
     Task<UserDataModel?> GetUserByPhoneNumber(string phoneNumber, CancellationToken ct = default);
     Task<UserDataModel?> GetUserByUserName(string userName, CancellationToken ct = default);

     Task<UserDataModel> GetUserByCriteria(Guid? id = null, string? userName = null,
         string? PhoneNumber = null, string? Email = null, CancellationToken ct = default);

     Task<PagedList<UserDataModel>> GetUsersByCriteria(
         Guid? id = null,
         string? userName = null,
         string? phoneNumber = null,
         string? email = null,
         int pageNumber = 1,
         int pageSize = 10,
         CancellationToken ct = default);
     Task<IEnumerable<UserDataModel>> GetAllUsersAsync(Expression<Func<UserDataModel, bool>> predicate = null, CancellationToken cancellationToken = default);
     Task<IPagedList<UserDataModel>> GetUsersByFilterAsync(UserType? type, AccountStatus? accountStatus, bool? isDeleted, int pageNumber, int pageSize, CancellationToken cancellationToken);
 }