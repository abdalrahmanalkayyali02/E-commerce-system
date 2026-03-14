using Catalog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Catalog.Domain.Repository.Write
{
    public interface IProductPhotoWriteRepository
    {
        public Task AddAsync(ProductPhotoEntity photo);
        public Task updateAsync (ProductPhotoEntity photo);
        public Task DeleteManyAsync(ProductPhotoEntity[] photo);
        public Task DeleteAsyncById(Guid id);
    }
}
