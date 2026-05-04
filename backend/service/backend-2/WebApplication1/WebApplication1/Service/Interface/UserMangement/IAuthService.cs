using WebApplication1.DTOs;
using WebApplication1.Shared.DTOs;
using WebApplication1.Shared.Result;

namespace WebApplication1.Service.Interface.UserMangement
{
    public interface IAuthService
    {
        public Task<Result<LoginUserResponse>> UserLogin(LoginDTos request,CancellationToken cancellationToken);
        public Task<Result<object>> GetMe(Guid userId,CancellationToken ct);
    }
}
