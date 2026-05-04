using WebApplication1.Data.Model;
using WebApplication1.Shared.Enum;

namespace WebApplication1.Repository.Interface;
public interface IUserOTpRepository
{
    public Task AddAsync(UserOtpDataModel userOtpEntity,CancellationToken cancellationToken = default);
    public void Update (UserOtpDataModel userOtpEntity, CancellationToken cancellationToken = default);

    public void SoftDelete (UserOtpDataModel userOTPEntity,CancellationToken cancellation = default);

    public Task<UserOtpDataModel?> GetLastOtpOfType
        (Guid userId, OtpType type, CancellationToken cancellation = default);
    
}