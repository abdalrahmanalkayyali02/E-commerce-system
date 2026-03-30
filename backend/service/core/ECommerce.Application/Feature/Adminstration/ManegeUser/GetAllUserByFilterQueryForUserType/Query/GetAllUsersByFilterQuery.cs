using Common.Collection;
using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Impl.Collection;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Adminstration.ManegeUser.GetAllUserByFilterQueryForUserType.Query
{
    public record GetAllUsersByFilterQuery(
        Guid adminID,
        int PageNumber = 1,
        int PageSize = 10,
        UserType? UserType = null 
    ) : IRequest<Result<IPagedList<UserDto>>>;
}
