using ECommerce.Application.Feature.Notification.usersNotification.GetAllNotifications.Query;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.Notification.usersNotification.GetAllNotifications.Validator
{
    public class GetAllNotificationQueryValidator : AbstractValidator<GetAllNotificationQuery>
    {
        public GetAllNotificationQueryValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.pageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");

            RuleFor(x => x.pageSize)
                .InclusiveBetween(1, 10).WithMessage("Page size must be between 1 and 10.");
        }
    }
}
