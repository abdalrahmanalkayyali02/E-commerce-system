using Common.DTOs.UserMangement.User;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Adminstration.ManegeUser.DeleteUserByCiteria.Command
{
    public record DeleteUserByCriteriaCommand
        (
        Guid adminID,
        Guid? Id = null,
        string? UserName = null,
        string? PhoneNumber = null,
        string? Email = null
        ) : IRequest<Result<UserDto>>;
 }

