using FluentValidation;
using ECommerce.Application.Feature.Adminstration.ManegeUser.GetAllUserByFilterQueryForUserType.Query;

namespace ECommerce.Application.Feature.Adminstration.ManegeUser.GetAllUserByFilterQueryForUserType.Validator
{
    public sealed class GetAllUsersByFilterQueryValidator : AbstractValidator<GetAllUsersByFilterQuery>
    {
        public GetAllUsersByFilterQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("The requested page number must be 1 or greater.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 10)
                .WithMessage("Page size must be between 1 and 10 items to optimize system performance.");

            RuleFor(x => x.UserType)
                .IsInEnum()
                .When(x => x.UserType.HasValue)
                .WithMessage("The provided User Type is not recognized by the system.");

            RuleFor(x => x.adminID)
                .NotEmpty()
                .WithMessage("Administrative identity is missing. Please ensure you are logged in with the correct permissions.");
        }
    }
}