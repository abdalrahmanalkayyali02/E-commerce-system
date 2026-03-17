namespace ECommerce.Domain.modules.Catalog.Repository
{
    public interface IProductCatogryRepository
    {
        Task<IReadOnlyList<CategoryItemRepresentation>> GetAllCatogryAsync(CancellationToken ct = default);

        Task<CategoryItemRepresentation?> GetCategoryByIdAsync(Guid id, CancellationToken ct = default);
    }

    public record CategoryItemRepresentation(
        Guid CategoryID,
        string CategoryName,
        Guid? ParentCategoryID
    );
}