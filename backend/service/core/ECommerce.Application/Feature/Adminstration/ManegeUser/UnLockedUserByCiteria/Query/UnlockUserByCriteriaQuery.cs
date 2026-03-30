using Common.DTOs.UserMangement.User;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Adminstration.ManegeUser.UnLockedUserByCiteria.Query
{
    public record UnlockUserByCriteriaQuery
    (
        Guid adminID,
        Guid? Id = null,
        string? UserName = null,
        string? PhoneNumber = null,
        string? Email = null
        ) : IRequest<Result<UserDto>>;
}
