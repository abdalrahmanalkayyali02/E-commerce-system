using Common.DTOs.UserMangement.User;
using Common.Impl.Collection;
using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Adminstration.ManegeUser.SearchByUserCriteria.Query
{
    public record SearchByUserCritecalQuery
        (
            Guid adminId,
            string? email = null,
            string? userName = null,
            string? phoneNumber = null,
            Guid? userID = null,
            int pageNumber = 1, 
            int pageSize = 10    
        ) : IRequest<Result<PagedList<UserDto>>>; 
}