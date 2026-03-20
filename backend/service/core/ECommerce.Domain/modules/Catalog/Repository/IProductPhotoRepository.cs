using ECommerce.Domain.modules.Catalog.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.Catalog.Repository
{
    public interface IProductPhotoRepository
    {
        public Task<ProductPhotoEntity?> GetProductPhotoById(Guid photoID);
        public Task AddAsync(ProductPhotoEntity photo);
        public Task updateAsync(ProductPhotoEntity photo);
        public Task DeleteManyAsync(ProductPhotoEntity[] photo);
        public Task DeleteAsyncById(Guid id);
    }

}
