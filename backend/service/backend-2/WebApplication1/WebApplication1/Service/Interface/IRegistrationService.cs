using WebApplication1.DTOs;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Interface
{
    public interface IRegistrationService
    {
        Task<Result<CreateUserResponse>> CreateCustomer(
            CreateCustomerDtOs request, 
            CancellationToken cancellationToken = default);

        Task<Result<CreateUserResponse>> CreateSeller(
            CreateSellerDtOs request, 
            CancellationToken cancellationToken = default);
    }
}