using Common.Specfication;
using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.modules.IAC.Repositories;
using ECommerce.Infrastructure.Persistence.Mapper;
using ECommerce.Infrastructure.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Persistence.Repository
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

        public async Task<UserEntity?> GetEntityWithSpec(ISpecification<UserEntity> spec, CancellationToken cancellationToken = default)
        {
            var query = _context.Users.AsNoTracking();

            return SpecificationEvaluator<UserEntity, UserDataModel>
                .GetQuery(query, spec, model => UserMapper.FromPersistenceToDomain(model))
                .FirstOrDefault(); 
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
    }
}
