using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Impl.Collection;
using Common.Impl.Result;
using MediatR;


namespace ECommerce.Application.Feature.Adminstration.ManegeUser.FilterUserByCriteria.Query
{
    public record FilterUserByCriteriaQuery(
            Guid adminId,
            UserType? userType = null,
            AccountStatus? accountStatus = null,
            bool? isDeleted = null,
            int pageNumber = 1,
            int pageSize = 10
        ) : IRequest<Result<PagedList<UserDto>>>;
}
