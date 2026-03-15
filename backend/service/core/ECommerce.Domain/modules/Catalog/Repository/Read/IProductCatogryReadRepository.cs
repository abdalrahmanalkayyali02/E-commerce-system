namespace ECommerce.Domain.modules.Catalog.Repository.Read
{
    public interface IProductCatogryReadRepository
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