using ECommerce.Domain.modules.Catalog.Entity;
using ECommerce.Domain.modules.Catalog.Value_Object;
using ECommerce.Infrastructure.Persistence.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Mapper.Catalog
{
    public static class ProductCategoryMapper
    {
        public static ProductCategoryModel FromDomainToPersistence(ProductCategoryEntity entity)
        {
            var model = new ProductCategoryModel();
            model.CategoryID = entity.CategoryID;
            model.Name = entity.CategoryName.Value;
            model.Description = entity.CategoryDescription.Value;
            model.ParentCategoryID = entity.CategoryID;
            model.isDeleted = entity.IsDelete;
            model.DeleteAt = entity.DeleteAt;
            model.CreatedAt = entity.CreateAT;
            model.UpdateAt = entity.UpdateAT;

            return model;
        }

        public static ProductCategoryEntity FromPersistenceToDomain(ProductCategoryModel model)
        {
            var entity = new ProductCategoryEntity
                (
                   id: model.CategoryID,
                   name: CategoryName.Create(model.Name).Value,
                   description:CategoryDescription.Create (model.Description).Value,
                   parentCategory: model.ParentCategoryID
                );

            return entity;
        }
    }
}
