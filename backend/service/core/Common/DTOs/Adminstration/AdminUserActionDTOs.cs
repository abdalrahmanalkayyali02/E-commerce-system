using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.Adminstration
{
    public record AdminUserActionDTOs(
        Guid? Id = null,
        string? UserName = null,
        string? PhoneNumber = null,
        string? Email = null
    );
}
