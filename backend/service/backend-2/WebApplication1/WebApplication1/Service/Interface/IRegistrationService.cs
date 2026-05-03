using WebApplication1.DTOs;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Interface
{
    public interface IRegistrationService
    {
        public Task<Result<CreateUserResponse>> CreateCustomer(CreateCustomerDtOs createCustomerDTOs);
        public Task<Result<CreateUserResponse>> CreateSeller(CreateSellerDtOs createSellerDTOs);
    }
}
