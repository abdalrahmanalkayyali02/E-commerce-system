using Common.DTOs.Notification;
using Common.Exceptions.System.NotificationMangement;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Notification.usersNotification.DeleteById.Command;
using MediatR;

namespace ECommerce.Application.Feature.Notification.usersNotification.DeleteById.Handler
{
    public sealed class DeleteNotificationByIdCommandHandler : IRequestHandler<DeleteNotificationByIdCommand, Result<NotificationDeleteResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNotificationByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<NotificationDeleteResponse>> Handle(DeleteNotificationByIdCommand command, CancellationToken ct)
        {
            try
            {
                var userExist = await _unitOfWork.Users.GetUserByID(command.reciverId, ct);
                if (userExist is null)
                    return Result<NotificationDeleteResponse>.Failure(UserIdAppError.NotFound);

                var notificationExist = await _unitOfWork.Notification.GetByIdAsync(command.notificationID, ct);
                if (notificationExist is null)
                    return Result<NotificationDeleteResponse>.Failure(NotificationAppError.NotFound);

                if (notificationExist.ReceiverId != command.reciverId)
                    return Result<NotificationDeleteResponse>.Failure(NotificationAppError.UnauthorizedAccess);

                notificationExist.MarkAsDeleted();

                _unitOfWork.Notification.Delete(notificationExist, ct);
                await _unitOfWork.SaveChangesAsync(ct);

                var response = new NotificationDeleteResponse("The notification was deleted successfully", notificationExist.notificationID);

                return Result<NotificationDeleteResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<NotificationDeleteResponse>.Failure(Error.Failure("System.Error", ex.Message));
            }
        }
    }
}