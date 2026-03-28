using Common.Specfication;
using ECommerce.Domain.modules.Catalog.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.Catalog.Repository
{
    public interface IProductRepository
    {
        public Task AddAsync (ProductEntity entity, CancellationToken cancellationToken=default);
        public void Update (ProductEntity entity, CancellationToken cancellationToken=default);
        public void SoftDelete (ProductEntity entity, CancellationToken cancellationToken=default);
        Task<ProductEntity?> GetEntityWithSpec(ISpecification<ProductEntity> spec, CancellationToken cancellationToken = default);

    }
}
