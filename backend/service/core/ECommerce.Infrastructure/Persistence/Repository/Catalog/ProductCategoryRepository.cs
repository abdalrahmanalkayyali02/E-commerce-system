using Common.Specfication;
using ECommerce.Domain.modules.Catalog.Entity;
using ECommerce.Domain.modules.Catalog.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Catalog
{
    internal class ProductCategoryRepository : IProductCatogryRepository

    {
        public Task AddAsync(ProductCategoryEntity entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CategoryItemRepresentation>> GetAllCatogryAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryItemRepresentation?> GetCategoryByIdAsync(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategoryEntity?> GetEntityWithSpec(ISpecification<ProductCategoryEntity> spec, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(ProductCategoryEntity entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductCategoryEntity entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
