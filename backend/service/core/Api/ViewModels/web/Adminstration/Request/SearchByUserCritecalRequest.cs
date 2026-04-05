using Common.DTOs.UserMangement.User;
using Common.Impl.Collection;
using Common.Impl.Result;
using MediatR;

namespace Api.ViewModels.web.Adminstration.Request
{

    public record SearchByUserCritecalRequest
    (
        string? email = null,
        string? userName = null,
        string? phoneNumber = null,
        Guid? userID = null,
        int pageNumber = 1,
        int pageSize = 10
    );
}
