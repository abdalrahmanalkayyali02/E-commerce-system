using Common.Reposotries;
using ECommerce.Domain.modules.IAC.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Abstraction.Data
{
    public interface IUnitOfWork : IDisposable
    {

        IUserRepository Users { get; }
        ICustomerRepository Customer { get; }
        ISellerRepository Seller { get; }
        IUserOTpRepository UserOTp { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
