using Common.Specfication;
using ECommerce.Domain.modules.Catalog.Entity;

namespace ECommerce.Domain.modules.Catalog.Repository
{
    public interface IProductCatogryRepository
    {
        Task<IReadOnlyList<CategoryItemRepresentation>> GetAllCatogryAsync(CancellationToken ct = default);
        Task<CategoryItemRepresentation?> GetCategoryByIdAsync(Guid id, CancellationToken ct = default);
        Task<ProductCategoryEntity?> GetEntityWithSpec(ISpecification<ProductCategoryEntity> spec, CancellationToken cancellationToken = default);

        Task AddAsync(ProductCategoryEntity entity, CancellationToken ct = default);

        void Update(ProductCategoryEntity entity, CancellationToken cancellationToken = default);
        void SoftDelete (ProductCategoryEntity entity, CancellationToken cancellationToken = default);
    }

    public record CategoryItemRepresentation(
        Guid CategoryID,
        string CategoryName,
        Guid? ParentCategoryID
    );
}