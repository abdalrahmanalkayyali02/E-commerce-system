using Common.Reposotries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Abstraction.Data
{
    public interface IUnitOfWork : IDisposable
    {
         IGenericRepository<T> Repo <T>() where T : class;

         Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
