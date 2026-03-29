using Common.DTOs.Notification;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Feature.Notification.usersNotification.GetAllNotifications.Query;
using FluentValidation;
using MediatR;

namespace ECommerce.Application.Feature.Notification.usersNotification.GetAllNotifications.Handler
{
    public sealed class GetAllNotificationQueryHandler : IRequestHandler<GetAllNotificationQuery, Result<IEnumerable<NotificationDetailDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetAllNotificationQuery> _validator;

        public GetAllNotificationQueryHandler(IUnitOfWork unitOfWork, IValidator<GetAllNotificationQuery> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<NotificationDetailDto>>> Handle(GetAllNotificationQuery query, CancellationToken ct)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(query, ct);
                if (!validationResult.IsValid)
                {
                    return Result<IEnumerable<NotificationDetailDto>>.Failure(
                        Error.Validation("Validation.Error", validationResult.Errors.First().ErrorMessage));
                }

                var userExist = await _unitOfWork.Users.GetUserByID(query.userId, ct);
                if (userExist is null)
                {
                    return Result<IEnumerable<NotificationDetailDto>>.Failure(UserIdAppError.NotFound);
                }

                var (notifications, totalCount) = await _unitOfWork.Notification
                    .GetPagedByUserIdAsync(query.userId, query.pageNumber, query.pageSize, ct);

                var notificationDtos = notifications.Select(n => new NotificationDetailDto(
                    n.notificationID,
                    n.ReceiverId,
                    n.Title,
                    n.Body,
                    n.Type,
                    n.Priority,
                    n.IsRead,
                    n.CreatedAt,
                    n.ReadAt
                ));

                return Result<IEnumerable<NotificationDetailDto>>.Success(notificationDtos);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<NotificationDetailDto>>.Failure(
                    Error.Failure("System.Error", ex.Message));
            }
        }
    }
}