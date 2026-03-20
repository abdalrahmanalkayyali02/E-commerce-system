using Common.DTOs.Catalog.Catogry.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.Catalog.Catogry.Request
{
    public record GetCatogryByIDRequest(Guid catogryID) : IRequest<GetCatogryByIdResponse>;
    
    
}
