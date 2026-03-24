using ECommerce.Domain.modules.Catalog.Entity;
using ECommerce.Domain.modules.Catalog.Value_Object;
using ECommerce.Infrastructure.Persistence.Model.Catalog;


namespace ECommerce.Infrastructure.Persistence.Mapper.Catalog
{
    public static class ProductPhotoMapper
    {
        public static ProductPhotoModel FromDomainToPersistence(ProductPhotoEntity entity)
        {
            var model = new ProductPhotoModel();
            model.id = entity.Id;
            model.url = entity.Url.Value;
            model.alterText = entity.AltText.Value;
            model.isMain = entity.IsMain;
            model.isDelete = entity.IsDelete;
            model.CreateAt = entity.CreateAt;
            model.UpdateAt = entity.UpdateAt;
            model.DeleteAt = entity.DeleteAt;
            model.isDelete = entity.IsDelete;

            return model;
        }

        public static ProductPhotoEntity FromPersistenceToDomain(ProductPhotoModel model)
        {
            var entity = new ProductPhotoEntity
                (
                    id: model.id,
                    url: PhotoUrl.Create(model.url).Value,
                    altText: AltText.Create(model.alterText).Value,
                    isMain: model.isMain
                );

            return entity;
        }
    }
}
