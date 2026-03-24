using ECommerce.Domain.modules.Catalog.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.Catalog.Repository
{
    public interface IProductAttributeRepository
    {
        public Task AddAsync (ProductAttributeEntity entity, CancellationToken cancellationToken);
        public void update (ProductAttributeEntity entity, CancellationToken cancellationToken);
        public void softDelete (ProductAttributeEntity entity, CancellationToken cancellationToken);
    }
}
