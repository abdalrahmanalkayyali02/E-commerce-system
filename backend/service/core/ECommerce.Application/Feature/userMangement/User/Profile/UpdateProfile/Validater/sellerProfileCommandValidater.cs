using ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Command;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using FluentValidation;

namespace ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Validater
{
    public class sellerProfileCommandValidater : AbstractValidator<sellerProfileCommand>
    {
        public sellerProfileCommandValidater()
        {
            RuleFor(x => x.FirstName)
                .Must(fname => !string.IsNullOrEmpty(fname) && !Name.FromStrict(fname).IsError)
                .WithMessage("First name must be 3-15 chars and contain no special characters.");

            RuleFor(x => x.LastName)
                .Must(lname => !string.IsNullOrEmpty(lname) && !Name.FromStrict(lname).IsError)
                .WithMessage("Last name must be 3-15 chars and contain no special characters.");

            RuleFor(x => x.phoneNumber)
                .Must(phone => string.IsNullOrEmpty(phone) || !PhoneNumber.From(phone).IsError)
                .WithMessage("Invalid phone number format.");

            RuleFor(x => x.address)
                .NotEmpty().WithMessage("Business address is required for sellers.")
                .Must(addr => !Address.Create(addr!).IsError)
                .WithMessage("Invalid business address format.");

            RuleFor(x => x.verifiedSellerDocument)
                .NotEmpty().WithMessage("Verification document is required for sellers.");
        }
    }
}