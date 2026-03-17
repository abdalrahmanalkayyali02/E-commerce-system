//using ECommerce.Application.DTO.Catalog.Catogry.Request;
//using ECommerce.Application.DTO.Catalog.Catogry.Response;
//using ECommerce.Application.Feature.Catalog.Queries.GetCatogry.Queries;
//using ECommerce.Domain.modules.Catalog.Repository.Read;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ECommerce.Application.Feature.Catalog.Queries.GetCatogry.Handler
//{
//    public class GetCatogryByIdQueriesHandler : IRequestHandler<GetCatorgyByIdQueries,GetCatogryByIdResponse>
//    {
//        private readonly IProductCatogryReadRepository _productCatogryReadRepository;

//        public GetCatogryByIdQueriesHandler(IProductCatogryReadRepository productCatogryReadRepository)

//        {
//            _productCatogryReadRepository = productCatogryReadRepository;
//        }       

//        public async Task<GetCatogryByIdResponse> Handle(GetCatorgyByIdQueries queries, CancellationToken cancellation)
//        {
//            var specficCatogry = await _productCatogryReadRepository.GetCategoryByIdAsync(queries.catogryID);

//            if (specficCatogry == null)
//            {
//                //return new GetCatogryByIdResponse()
//            }

//        }
//    }
//}
