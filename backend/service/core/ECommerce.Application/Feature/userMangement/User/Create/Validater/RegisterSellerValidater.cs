using FluentValidation;
using System;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using ECommerce.Application.Feature.userMangement.User.Create.Command;

namespace ECommerce.Application.Feature.userMangement.User.Create.Validater
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



            RuleFor(x => x.shopName)
                .NotEmpty()
                .WithMessage("Shop name is required.")
                .MaximumLength(20)
                .WithMessage("The max length for the shop name is 20 characters.");

        
        }
    }
}