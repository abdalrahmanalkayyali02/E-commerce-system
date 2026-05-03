using WebApplication1.Data.Model;

namespace WebApplication1.Repository.Interface;

public interface ICustomerRepository
{
    public Task AddAsync(CustomerDataModel entity,CancellationToken cancellation= default);
    public void Update(CustomerDataModel entity, CancellationToken cancellation = default);
    Task<CustomerDataModel?> GetUserById(Guid id, CancellationToken ct = default);

}