using Common.Enum;
using Common.Impl.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.User.Profile
{
    public interface IProfileStrategy
    {
        UserType userType { get; }
        Task<Result<object>> GetProfileAsync(Guid id, CancellationToken ct);

        Task<Result<object>> UpdateProfile(object data, CancellationToken ct);
        
    }
}
