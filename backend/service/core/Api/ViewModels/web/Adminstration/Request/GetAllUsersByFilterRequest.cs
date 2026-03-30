using Common.Enum;

namespace Api.ViewModels.web.Adminstration.Request
{
    public record GetAllUsersByFilterRequest(
        int PageNumber = 1,
        int PageSize = 10,
        UserType? UserType = null);
}
