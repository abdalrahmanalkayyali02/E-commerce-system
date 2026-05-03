using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Interface
{
    public interface IAuthService
    {
        public Result<LoginUserResponse> UserLogin(string email, string password);
        public Result<object> GetMe(Guid userId);
    }
}
