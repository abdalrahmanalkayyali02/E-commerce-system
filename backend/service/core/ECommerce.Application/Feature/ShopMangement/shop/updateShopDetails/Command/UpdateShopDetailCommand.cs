using Common.DTOs.ShopMangement;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.ShopMangement.shop.updateShopDetails.Command
{
    public record UpdateShopDetailCommand
        (
           Guid sellerID,string? shopName, Stream? shopPhoto, Stream? verfiedSellerDocument, Stream? verfiedShopDocument
        ) : IRequest<Result<updateShopDetailsResponse>>;


}
