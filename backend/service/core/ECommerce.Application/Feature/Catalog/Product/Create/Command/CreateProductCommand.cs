using Common.DTOs.Catalog;
using Common.Impl.Result;
using MediatR;


namespace ECommerce.Application.Feature.Catalog.Product.Create.Command
{
    public record CreateProductCommand(
        Guid CategoryId,
        Guid SellerId,
        string ProductName,
        string ProductDescription,
        decimal BasePrice,
        List<ProductPhotoDto> ProductPhotos,
        List<ProductAttributeDto> ProductAttributes
    ) : IRequest<Result<Guid>>;
}