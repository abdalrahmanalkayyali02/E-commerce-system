using Catalog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Repository.Read
{
    public interface IProductPhotoReadRepository
    {
        public Task<ProductPhotoEntity?> GetProductPhotoById(Guid photoID);
    }

}
