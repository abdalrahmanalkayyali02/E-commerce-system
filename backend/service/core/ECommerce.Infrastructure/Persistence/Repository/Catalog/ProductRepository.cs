using Common.Specfication;
using ECommerce.Domain.modules.Catalog.Entity;
using ECommerce.Domain.modules.Catalog.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Catalog
{
    public class ProductRepository : IProductRepository
    {
        public Task AddAsync(ProductEntity entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProductEntity?> GetEntityWithSpec(ISpecification<ProductEntity> spec, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(ProductEntity entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductEntity entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
