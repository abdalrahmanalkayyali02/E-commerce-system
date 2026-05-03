using WebApplication1.Data.Model;

namespace WebApplication1.Repository.Interface;

public interface ISellerRepository
{
    Task AddAsync(SellerDataModel entity, CancellationToken cancellation = default);

    void Update(SellerDataModel entity, CancellationToken cancellation = default);
    public Task<SellerDataModel?> GetSellerWithId(Guid id, CancellationToken cancellation = default);


    Task<IEnumerable<SellerDataModel>> GetAllSellerNotVerfiedSellerDocument(CancellationToken cancellationToken = default);

    Task<IEnumerable<SellerDataModel>> GetAllSellerNotVerfiedShopDocument(CancellationToken cancellationToken = default);

    Task<IEnumerable<SellerDataModel>> GetAllSellerNotVerfiedByAdmin(CancellationToken cancellationToken = default);

    Task<IEnumerable<SellerDataModel>> GetAllSellerThatVerifedByAdmin(CancellationToken cancellationToken = default);
}
