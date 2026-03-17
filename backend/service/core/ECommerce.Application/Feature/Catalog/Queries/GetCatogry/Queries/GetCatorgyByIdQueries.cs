using Common.DTOs.Catalog.Catogry.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Catalog.Queries.GetCatogry.Queries
{
    
    public record GetCatorgyByIdQueries(Guid catogryID) : IRequest<GetCatogryByIdResponse>;
}
