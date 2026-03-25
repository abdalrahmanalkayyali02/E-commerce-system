using Common.DTOs.IAC.User;
using Common.Enum;
using Common.Impl.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.IAC.User.GetProfile.Strategy
{
    public interface IProfileStrategy
    {
        UserType userType { get; }
        Task<Result<UserResponse>> GetProfileAsync(Guid id, CancellationToken ct);
    }
}
