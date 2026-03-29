using Common.DTOs.Notification;
using Common.Exceptions.System.NotificationMangement;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Notification.usersNotification.GetById.Queries;
using MediatR;

namespace ECommerce.Application.Feature.Notification.usersNotification.GetById.Handler
{
    public sealed class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, Result<NotificationDetailDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNotificationByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<NotificationDetailDto>> Handle(GetNotificationByIdQuery query, CancellationToken ct)
        {
            try
            {
                var userExist = await _unitOfWork.Users.GetUserByID(query.userID);
                if (userExist is null)
                    return Result<NotificationDetailDto>.Failure(UserIdAppError.NotFound);

                var notification = await _unitOfWork.Notification.GetByIdAsync(query.id);
                if (notification is null)
                    return Result<NotificationDetailDto>.Failure(NotificationAppError.NotFound);

                if (notification.ReceiverId != query.userID)
                    return Result<NotificationDetailDto>.Failure(NotificationAppError.UnauthorizedAccess);

                var notificationDto = new NotificationDetailDto(
                    notification.notificationID,
                    notification.ReceiverId,
                    notification.Title,
                    notification.Body,
                    notification.Type,
                    notification.Priority,
                    notification.IsRead,
                    notification.CreatedAt,
                    notification.ReadAt
                );

                // save at db
                _unitOfWork.Notification.Update(notification, ct);
                await _unitOfWork.SaveChangesAsync(ct);

                return Result<NotificationDetailDto>.Success(notificationDto);
            }
            catch (Exception ex)
            {
                return Result<NotificationDetailDto>.Failure(Error.Failure("System.Error", ex.Message));
            }
        }
    }
}