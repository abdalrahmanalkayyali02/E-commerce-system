using Common.Impl.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Notification.usersNotification.GetUnreadCountNotification.Query
{
    public record GetUnreadCountQuery(Guid userId) : IRequest<Result<int>>;
    
    
}
