using WebApplication1.Shared.Enum;

namespace WebApplication1.Shared.DTOs
{
    public record LoginUserResponse( bool VerfiedEmail, AccountStatus AccountStatus, string token);
}
