using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.modules.IAC.ValueObject;
using ECommerce.Infrastructure.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Mapper
{
    public static class SellerMapper
    {
        // Domain => Persistence 

        public static SellerDataModel FromDomainToPersistence (SellerEntity entity)
        {
            var model = new SellerDataModel ();

            model.sellerID = entity.sellerID;
            model.shopName = entity.shopName;
            model.shopPhoto = entity.shopPhoto;
            model.address = entity.address.Value;
            model.isVerifiedByAdmin = entity.isVerifiedByAdmin;
            model.verfiedSellerDocument = entity.verfiedSellerDocument;
            model.isVerfiedSellerDocumentBeenViewed = entity.isVerfiedSellerDocumentBeenViewed;
            model.verfiedShopDocument = entity.verfiedShopDocument;
            model.isVerfiedShopDocumentBeenViewed = entity.isVerfiedShopDocumentBeenViewed;
            model.CreateAt = entity.CreateAt;
            model.UpdateAt = entity.UpdateAt;

            return model;
        }

        public static SellerEntity FromPersistenceToDomain(SellerDataModel model)
        {
            var seller = new SellerEntity
                (
                    id : model.sellerID,
                    shopName: model.shopName,
                    shopPhoto: model.shopPhoto,
                    address: Address.Reconstruct(model.address),
                    isVerfiedByAdmin: model.isVerifiedByAdmin,
                    verfiedShopDocument: model.verfiedShopDocument,
                    VerfiedSelllerDocument: model.verfiedSellerDocument,
                    isShopDocumentView: model.isVerfiedShopDocumentBeenViewed,
                    isSellerDocumentView: model.isVerfiedSellerDocumentBeenViewed
                );

            return seller;
           
        }

    }
}
