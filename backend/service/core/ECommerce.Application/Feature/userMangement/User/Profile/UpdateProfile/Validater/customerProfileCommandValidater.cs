using ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Command;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using FluentValidation;

namespace ECommerce.Application.Feature.userMangement.User.Profile.UpdateProfile.Validater
{
    public class customerProfileCommandValidater : AbstractValidator<customerProfileCommand>
    {
        public customerProfileCommandValidater()
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
                .Must(addr => string.IsNullOrEmpty(addr) || !Address.Create(addr).IsError)
                .WithMessage("Invalid address format.");
        }
    }
}