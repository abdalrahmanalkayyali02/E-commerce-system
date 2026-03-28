using ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Command;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using FluentValidation;

namespace ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Validater
{
    public class userProfileCommandValidater : AbstractValidator<userProfileCommand>
    {
        public userProfileCommandValidater()
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

            RuleFor(x => x.profilePhoto)
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrEmpty(x.profilePhoto))
                .WithMessage("Profile photo must be a valid URL.");
        }

        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}