using ECommerce.Domain.modules.Catalog.Entity;
using ECommerce.Domain.modules.Catalog.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.Catalog
{
    internal class ProductPhotoRepository : IProductPhotoRepository
    {
        public Task AddAsync(ProductPhotoEntity photo, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public void DeleteManyAsync(ProductPhotoEntity[] photo, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProductPhotoEntity?> GetProductPhotoById(Guid photoID, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(ProductPhotoEntity entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public void updateAsync(ProductPhotoEntity photo, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
