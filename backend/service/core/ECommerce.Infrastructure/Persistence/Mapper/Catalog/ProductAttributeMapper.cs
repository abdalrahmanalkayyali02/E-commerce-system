using ECommerce.Domain.modules.Catalog.Entity;
using ECommerce.Domain.modules.Catalog.Value_Object;
using ECommerce.Infrastructure.Persistence.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Mapper.Catalog
{
    public static class ProductAttributeMapper
    {
        public static ProductAttributeModel FromDomainToPersistence (ProductAttributeEntity entity)
        {
            var model = new ProductAttributeModel ();
            model.ProductAttributeID = entity.ProductAttributeID;
            model.AttributeName = entity.AttributeName.Value;
            model.AttributeValue = entity.AttributeValue.Value;
            model.Unit = entity.Unit?.Value;
            model.isFilterable = entity.IsFilterable;
            model.isVariant = entity.IsVariant;
            model.DisplayOrder = entity.DisplayOrder;
            model.isDelete = entity.IsDelete;
            model.CreateAt = entity.CreateAt;
            model.UpdateAt = entity.UpdateAt;
            model.DeleteAt = entity.DeleteAt;

            return model;
        } 

        public static ProductAttributeEntity FromPersistenceToDomain(ProductAttributeModel model)
        {
            var entity = new ProductAttributeEntity
                (
                    id: model.ProductAttributeID,
                    name: AttributeName.Create(model.AttributeName).Value,
                    value: AttributeValue.Create(model.AttributeValue).Value,
                    isFilterable: model.isFilterable,
                    isVariant: model.isVariant,
                    displayOrder: model.DisplayOrder,
                    unit: Unit.Create(model.Unit).Value
                );

            return entity;
        }
    }


}
