//using Common.DTOs.Catalog.Catogry.Response;
//using ECommerce.Domain.modules.Catalog.Repository;
//using MediatR;

//namespace ECommerce.Application.Feature.Catalog.Queries.GetCatogry.Handler
//{
//    public record GetAllCategoriesQuery : IRequest<GetAllCatogryResponse>;

//    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, GetAllCatogryResponse>
//    {
//        private readonly IProductCatogryRepository _categoryReadRepo;

//        public GetAllCategoriesHandler(IProductCatogryRepository categoryReadRepo)
//        {
//            _categoryReadRepo = categoryReadRepo;
//        }

//        public async Task<GetAllCatogryResponse> Handle(GetAllCategoriesQuery query, CancellationToken ct)
//        {
//            var categories = await _categoryReadRepo.GetAllCatogryAsync(ct);

//            if (categories == null)
//            {
//                return new GetAllCatogryResponse(new List<CategoryItemDTO>());
//            }

//            var dtoList = categories.Select(c => new CategoryItemDTO(
//                c.CategoryID,
//                c.CategoryName,
//                c.ParentCategoryID
//            )).ToList();

//            return new GetAllCatogryResponse(dtoList);
//        }
//    }
//}