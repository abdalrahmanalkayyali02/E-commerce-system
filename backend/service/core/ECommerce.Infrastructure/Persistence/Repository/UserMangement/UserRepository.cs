using Common.Collection;
using Common.Enum;
using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;
using ECommerce.Domain.modules.UserMangement.Repositories;
using ECommerce.Infrastructure.Persistence.Mapper.UserMangement;
using ECommerce.Infrastructure.Persistence.Model.UserMangement;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Persistence.Repository.UserMangement
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserEntity user, CancellationToken cancellation = default)
        {
            var model = UserMapper.FromDomainToPersistence(user);

            await _context.Users.AddAsync(model, cancellation);
        }

        public async Task DeleteAsync(Guid id)
        {
            var model =  await _context.Users.FindAsync(id);

            if (model != null)
            {
                _context.Users.Remove(model);
            }
        }


        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync(Expression<Func<UserEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Users.AsNoTracking().AsQueryable();

            var entityQuery = query.Select(m => UserMapper.FromPersistenceToDomain(m));

            if (predicate != null)
            {
                entityQuery = entityQuery.Where(predicate);
            }

            return await entityQuery.ToListAsync(cancellationToken);
        }

        public async Task<UserEntity?> GetEntityWithSpec(
            ISpecification<UserDataModel> spec, // Change the Spec to target the DataModel
            CancellationToken cancellationToken = default)
        {
            try
            {
                IQueryable<UserDataModel> query = _context.Users.AsNoTracking();

                var evaluatedQuery = SpecificationEvaluator<UserDataModel>.GetQuery(query, spec);

                var dataModel = await evaluatedQuery.FirstOrDefaultAsync(cancellationToken);

                return dataModel != null ? UserMapper.FromPersistenceToDomain(dataModel) : null;
            }
            catch (Exception)
            {
                // Log your exception here if needed
                return null;
            }
        }

        public async Task<UserEntity?> GetUserByEmail(string email, CancellationToken ct = default)
        {
            var model = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && !u.isDelete, ct);

            return model != null ? UserMapper.FromPersistenceToDomain(model) : null;
        }

        public async Task<UserEntity?> GetUserByUserName(string username, CancellationToken ct = default)
        {
            var model = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == username && !u.isDelete, ct);

            return model != null ? UserMapper.FromPersistenceToDomain(model) : null;
        }

        public async Task<UserEntity?> GetUserByPhonNumber(string phoneNumber, CancellationToken ct = default)
        {
            var model = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.phoneNumber == phoneNumber && !u.isDelete, ct);

            return model != null ? UserMapper.FromPersistenceToDomain(model) : null;
        }

        public async Task<UserEntity?> GetUserByID(Guid id, CancellationToken ct = default)
        {
            var model = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.id == id && !u.isDelete, ct);

            return model != null ? UserMapper.FromPersistenceToDomain(model) : null;
        }


        public void  SoftDelete(UserEntity user, CancellationToken cancellation = default)
        {
            var model = UserMapper.FromDomainToPersistence(user);

             _context.Users.Update(model);
        }

        public void Update(UserEntity user, CancellationToken cancellation = default)
        {
            var model = UserMapper.FromDomainToPersistence(user);

            _context.Users.Update(model);
        }

        public Task<UserEntity?> GetEntityWithSpec(ISpecification<UserEntity> spec, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity?> GetUserByPhoneNumber(string phoneNumber, CancellationToken ct = default)
        {
            var model = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.phoneNumber == phoneNumber && !u.isDelete, ct);

            return model != null ? UserMapper.FromPersistenceToDomain(model) : null;
        }

        public Task<UserEntity?> GetUserByCriteria(Guid? id = null, string? userName = null, string? PhoneNumber = null, string? Email = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<UserEntity>> GetUsersByFilterAsync(UserType? type, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
