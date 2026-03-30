using Common.DTOs.Adminstration.Response;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Adminstration.ManegeShop.VerfiedSeller.Command
{
    public record VerfiedSellerCommand(Guid adminId, Guid sellerID) : IRequest<Result<VerfiedSellerResponse>>;
    
    
}
