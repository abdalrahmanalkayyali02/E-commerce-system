using FluentValidation;
using ECommerce.Domain.modules.IAC.ValueObject;
using ECommerce.Application.Feature.IAC.User.Create.Command;
using System;

namespace ECommerce.Application.Feature.IAC.User.Create.Validater
{
    public class CreateSellerValidater : AbstractValidator<CreateSellerCommand>
    {
        public CreateSellerValidater()
        {
            RuleFor(x => x.firstName)
               .NotEmpty()
               .Must(name => !Name.FromStrict(name).IsError)
               .WithMessage("First name must be 4-15 chars and contain no special characters.");

            RuleFor(x => x.lastName)
                .NotEmpty()
                .Must(name => !Name.FromStrict(name).IsError)
                .WithMessage("Last name must be 4-15 chars and contain no special characters.");

            RuleFor(x => x.userName)
                .NotEmpty()
                .Must(name => !Name.From(name).IsError)
                .WithMessage("Username allows only one '_' or '@' and must be 4-15 chars.");

            RuleFor(x => x.address)
                .NotEmpty()
                .Must(address => !Address.Create(address).IsError)
                .WithMessage("Invalid address format.");

            RuleFor(x => x.email)
                .NotEmpty()
                .Must(email => !Email.From(email).IsError)
                .WithMessage("Invalid email format.");

            RuleFor(x => x.password)
                .NotEmpty()
                .Must(password => !Password.From(password).IsError)
                .WithMessage("Password requires 8+ chars, uppercase, and digits.");

            RuleFor(x => x.dateOfBirth)
                .NotEmpty()
                .Must(date => !DateOfBirth.From(date).IsError)
                .WithMessage("Seller must be at least 18 years old and under 150.");

            RuleFor(x => x.profilePhoto)
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Profile photo must be a valid URL.");

            RuleFor(x => x.shopName)
                .NotEmpty()
                .WithMessage("Shop name is required.")
                .MaximumLength(20)
                .WithMessage("The max length for the shop name is 20 characters.");

            RuleFor(x => x.shopPhoto)
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Shop photo must be a valid URL.");

            RuleFor(x => x.verfiedSellerDocument)
                .NotEmpty().WithMessage("Seller verification document is required.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Seller document must be a valid URL.");

            RuleFor(x => x.verfiedShopDocument)
                .NotEmpty().WithMessage("Shop verification document is required.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Shop document must be a valid URL.");
        }
    }
}