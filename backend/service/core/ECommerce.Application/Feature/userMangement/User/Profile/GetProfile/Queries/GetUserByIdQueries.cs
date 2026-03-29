using Common.Enum;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.User.Profile.GetProfile.Queries
{
    public record GetUserByIdQueries(Guid id,UserType userType) : IRequest<Result<Object>>;
    
    
}
