using ECommerce.Domain.modules.Catalog.Entity;
using ECommerce.Domain.modules.Catalog.Value_Object;
using ECommerce.Infrastructure.Persistence.Model.Catalog;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Infrastructure.Persistence.Mapper.Catalog
{
    public static class ProductMapper
    {
        public static ProductModel FromDomainToPersistence(ProductEntity entity)
        {
            if (entity == null) return null;

            var model = new ProductModel();

            model.ProductID = entity.Id;
            model.CategouryID = entity.CategoryId;
            model.SellerID = entity.SellerId;
            model.productName = entity.Name.Value;
            model.prodcutDescription = entity.Description.Value;
            model.BasePrice = entity.BasePrice;
            model.isActive = entity.IsActive;
            model.isDeleted = entity.IsDeleted;
            model.CreateAt = entity.CreatedAt;
            model.UpdateAt = entity.UpdatedAt ?? System.DateTime.UtcNow;
            model.DeleteAt = entity.DeletedAt;

            model.ProductPhotoModels = entity.Photos
                .Select(ProductPhotoMapper.FromDomainToPersistence)
                .ToList();

            model.ProductAttributes = entity.Attributes
                .Select(ProductAttributeMapper.FromDomainToPersistence)
                .ToList();

            return model;
        }

        public static ProductEntity FromPersistenceToDomain(ProductModel model)
        {
            if (model == null) return null;

            var photoEntities = model.ProductPhotoModels
                .Select(ProductPhotoMapper.FromPersistenceToDomain)
                .ToList();

            var attributeEntities = model.ProductAttributes
                .Select(ProductAttributeMapper.FromPersistenceToDomain)
                .ToList();


            var entity = new ProductEntity(
                id: model.ProductID,
                categoryId: model.CategouryID,
                sellerId: model.SellerID,
                name: ProductName.Create(model.productName).Value,
                description: ProductDescription.Create(model.prodcutDescription).Value,
                basePrice: model.BasePrice,
                photos: photoEntities
            );

            foreach (var attr in attributeEntities)
            {
                entity.AddAttribute(attr);
            }

            return entity;
        }
    }
}