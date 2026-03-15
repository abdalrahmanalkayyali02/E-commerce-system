using Common.Abstraction.Reposotries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public IWriteReposotries<T> WriteRepository<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
