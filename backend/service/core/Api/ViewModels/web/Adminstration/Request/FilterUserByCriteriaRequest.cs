using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Impl.Collection;
using Common.Impl.Result;
using MediatR;

namespace Api.ViewModels.web.Adminstration.Request
{
    public record FilterUserByCriteriaRequest(
        UserType? userType = null,
        AccountStatus? accountStatus = null,
        bool? isDeleted = null,
        int pageNumber = 1,
        int pageSize = 10
    );
}
