using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Common.Reposotries
{
    public class GenericRepsotries<TDbContext, TModel> : IGenericRepository<TModel>
        where TModel : class
        where TDbContext : DbContext
    {
        protected readonly TDbContext _db;
        protected readonly DbSet<TModel> _model;

        public GenericRepsotries(TDbContext db)
        {
            _db = db;
            _model = db.Set<TModel>();
        }

        public async Task AddAsync(TModel entity, CancellationToken cancellationToken = default)
        {
            await _model.AddAsync(entity, cancellationToken);
        }

        public async Task<TModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _model.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _model.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TModel?> FindAsync(Expression<Func<TModel, bool>> predicate, CancellationToken ct = default)
        {
            return await _model.AsNoTracking().FirstOrDefaultAsync(predicate, ct);
        }

        public void Update(TModel entity)
        {
            _model.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                _model.Remove(entity);
            }
        }

        public void SoftDelete(TModel entity)
        {
            _model.Attach(entity);

            var property = _db.Entry(entity).Property("IsDeleted");
            if (property != null)
            {
                property.CurrentValue = true;
                _db.Entry(entity).State = EntityState.Modified;
            }
        }

     
    }
}