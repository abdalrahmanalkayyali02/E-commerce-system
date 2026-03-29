using Common.DTOs.Notification;
using Common.Exceptions.System.NotificationMangement;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Notification.usersNotification.MarkNotificationByIdAsRead.Command;
using MediatR;

namespace ECommerce.Application.Feature.Notification.usersNotification.MarkNotificationByIdAsRead.Handler
{
    public sealed class MarkNotificationByIdAsReadCommandHandler : IRequestHandler<MarkNotificationByIdAsReadCommand, Result<NotificationMarkasReadStatusResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkNotificationByIdAsReadCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<NotificationMarkasReadStatusResponse>> Handle(MarkNotificationByIdAsReadCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var userExist = await _unitOfWork.Users.GetUserByID(command.receiverID, cancellationToken);
                if (userExist is null)
                    return Result<NotificationMarkasReadStatusResponse>.Failure(UserIdAppError.NotFound);

                var notification = await _unitOfWork.Notification.GetByIdAsync(command.id, cancellationToken);
                if (notification is null)
                    return Result<NotificationMarkasReadStatusResponse>.Failure(NotificationAppError.NotFound);

                if (notification.ReceiverId != command.receiverID)
                    return Result<NotificationMarkasReadStatusResponse>.Failure(NotificationAppError.UnauthorizedAccess);

                notification.MarkAsRead();

                _unitOfWork.Notification.Update(notification);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var response = new NotificationMarkasReadStatusResponse(
                    "Notification marked as read successfully",
                    notification.notificationID,
                    notification.IsRead
                );

                return Result<NotificationMarkasReadStatusResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<NotificationMarkasReadStatusResponse>.Failure(Error.Failure("System.Error", ex.Message));
            }
        }
    }
}