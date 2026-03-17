using Common.Reposotries;
using ECommerce.Application.Abstraction.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ECommerce.Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private Hashtable _repositories; 
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> Repo<T>() where T : class
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new GenericRepsotries<DbContext, T>(_context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[type]!;
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}