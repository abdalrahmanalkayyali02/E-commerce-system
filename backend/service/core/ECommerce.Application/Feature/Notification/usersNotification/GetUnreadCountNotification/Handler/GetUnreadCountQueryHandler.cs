using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Notification.usersNotification.GetUnreadCountNotification.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Notification.usersNotification.GetUnreadCountNotification.Handler
{
    public sealed class GetUnreadCountQueryHandler : IRequestHandler<GetUnreadCountQuery, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUnreadCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<Result<int>> Handle(GetUnreadCountQuery query, CancellationToken ct)
        {
            try
            {
                var userExist = await _unitOfWork.Users.GetUserByID(query.userId, ct);
                if (userExist is null)
                    return Result<int>.Failure(UserIdAppError.NotFound);

                var count = await _unitOfWork.Notification.GetUnreadCountByUserIdAsync(query.userId, ct);

                return Result<int>.Success(count);
            } 
            catch (Exception ex)
            {
                return Result<int>.Failure(Error.Failure("System.Error", ex.Message));
            }
        }
    }
}
