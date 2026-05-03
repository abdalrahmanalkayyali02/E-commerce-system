namespace WebApplication1.Repository.Interface;

public interface IUnitOfWork : IDisposable
{

    IUserRepository Users { get; }
    ICustomerRepository Customer { get; }
    ISellerRepository Seller { get; }
    IUserOTpRepository UserOTp { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}