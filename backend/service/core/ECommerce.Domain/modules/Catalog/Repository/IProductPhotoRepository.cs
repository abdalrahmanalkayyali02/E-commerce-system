using ECommerce.Domain.modules.Catalog.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.Catalog.Repository
{
    public interface IProductPhotoRepository
    {
        public Task<ProductPhotoEntity?> GetProductPhotoById(Guid photoID,CancellationToken ct=default);
        public Task AddAsync(ProductPhotoEntity photo, CancellationToken ct=default);
        public void updateAsync(ProductPhotoEntity photo,CancellationToken ct=default);
        public void DeleteManyAsync(ProductPhotoEntity[] photo,CancellationToken ct= default);
        public void SoftDelete(ProductPhotoEntity entity,CancellationToken ct=default);
    }

}
