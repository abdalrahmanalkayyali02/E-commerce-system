using ECommerce.Domain.modules.Catalog.Entity;
using ECommerce.Domain.modules.Catalog.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Catalog
{
    internal class ProductAttributeRepository : IProductAttributeRepository
    {
        public Task AddAsync(ProductAttributeEntity entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void softDelete(ProductAttributeEntity entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void update(ProductAttributeEntity entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
