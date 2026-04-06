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
                .Must(fname => !Name.FromStrict(fname!).IsError)
                .When(x => !string.IsNullOrEmpty(x.FirstName))
                .WithMessage("First name must be 3-15 chars and contain no special characters.");

            RuleFor(x => x.LastName)
                .Must(lname => !Name.FromStrict(lname!).IsError)
                .When(x => !string.IsNullOrEmpty(x.LastName))
                .WithMessage("Last name must be 3-15 chars and contain no special characters.");

            RuleFor(x => x.phoneNumber)
                .Must(phone => !PhoneNumber.From(phone!).IsError)
                .When(x => !string.IsNullOrEmpty(x.phoneNumber))
                .WithMessage("Invalid phone number format.");

            RuleFor(x => x.address)
                .Must(addr => !Address.Create(addr!).IsError)
                .When(x => !string.IsNullOrEmpty(x.address))
                .WithMessage("Invalid business address format.");

            RuleFor(x => x.profilePhoto)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.profilePhoto))
                .WithMessage("Profile photo must be a valid URL.");
        }
    }
}