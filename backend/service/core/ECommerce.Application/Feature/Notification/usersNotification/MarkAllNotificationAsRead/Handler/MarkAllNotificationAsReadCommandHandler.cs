using Common.DTOs.Notification;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Notification.usersNotification.MarkAllNotificationAsRead.Command;
using MediatR;

namespace ECommerce.Application.Feature.Notification.usersNotification.MarkAllNotificationAsRead.Handler
{
    public sealed class MarkAllNotificationAsReadCommandHandler : IRequestHandler<MarkAllNotificationAsReadCommand, Result<MarksAllNotificatonAsReadStatusResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkAllNotificationAsReadCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MarksAllNotificatonAsReadStatusResponse>> Handle(MarkAllNotificationAsReadCommand command, CancellationToken ct)
        {
            try
            {
                var userExist = await _unitOfWork.Users.GetUserByID(command.userID, ct);
                if (userExist == null)
                {
                    return Result<MarksAllNotificatonAsReadStatusResponse>.Failure(UserIdAppError.NotFound);
                }


                await _unitOfWork.Notification.MarkAllAsReadByUserIdAsync(command.userID, ct);

                await _unitOfWork.SaveChangesAsync(ct);
                

                var response = new MarksAllNotificatonAsReadStatusResponse(
                    "All notifications have been marked as read.",
                    command.userID
                );

                return Result<MarksAllNotificatonAsReadStatusResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<MarksAllNotificatonAsReadStatusResponse>.Failure(Error.Failure("System.Error", ex.Message));
            }
        }
    }
}