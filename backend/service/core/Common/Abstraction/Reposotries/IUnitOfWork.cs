using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Abstraction.Reposotries
{
    
        public interface IUnitOfWork : IDisposable
        { 
            IWriteReposotries<T> WriteRepository<T>() where T : class;
            Task<int> SaveChangesAsync(CancellationToken cancellation);
        }
}


