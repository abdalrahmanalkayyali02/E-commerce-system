using ECommerce.Application.DTO.Catalog.Catogry.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTO.Catalog.Catogry.Request
{
    
    public record GetAllCatogryRequest : IRequest<GetAllCatogryResponse>;
}
