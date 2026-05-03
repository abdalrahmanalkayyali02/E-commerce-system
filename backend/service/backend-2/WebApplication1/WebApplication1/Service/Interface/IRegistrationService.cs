using WebApplication1.DTOs;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Interface
{
    public interface IRegistrationService
    {
        public Task<Result<CreateUserResponse>> CreateCustomer(CreateCustomerDTOs createCustomerDTOs);
        public Task<Result<CreateUserResponse>> CreateSeller(CreateSellerDTOs createSellerDTOs);
    }
}
