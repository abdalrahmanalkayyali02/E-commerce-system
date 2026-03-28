using Common.Reposotries;
using ECommerce.Domain.modules.Catalog.Repository;
using ECommerce.Domain.modules.UserMangement.Repositories;
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
        IProductRepository productRepository { get; }
        IProductAttributeRepository ProductAttributeRepository { get; }
        IProductCatogryRepository ProductCatogryRepository { get; }
        IProductPhotoRepository ProductPhotoRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
