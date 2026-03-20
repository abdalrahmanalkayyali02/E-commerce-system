using FluentValidation;
using ECommerce.Domain.modules.IAC.ValueObject;
using ECommerce.Application.Feature.IAC.User.Create.Command;
using System;

namespace ECommerce.Application.Feature.IAC.User.Create.Validater
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.firstName)
                .NotEmpty()
                .Must(BeAValidFirstOrLastName)
                .WithMessage("First name must be 4-15 chars and contain no special characters.");

            RuleFor(x => x.lastName)
                .NotEmpty()
                .Must(BeAValidFirstOrLastName)
                .WithMessage("Last name must be 4-15 chars and contain no special characters.");

            RuleFor(x => x.userName)
                .NotEmpty()
                .Must(BeAValidUserName)
                .WithMessage("Username allows only one '_' or '@' and must be 4-15 chars.");

            RuleFor(x => x.address)
                .NotEmpty()
                .Must(BeAValidAddress)
                .WithMessage("Invalid address format.");

            RuleFor(x => x.email)
                .NotEmpty()
                .Must(BeAValidEmail)
                .WithMessage("Invalid email format.");

            RuleFor(x => x.password)
                .NotEmpty()
                .Must(BeAValidPassword)
                .WithMessage("Password requires 8+ chars, uppercase, and digits.");

            // التعديل: نمرر الـ date مباشرة لأن الـ Command صار فيه DateOnly
            RuleFor(x => x.dateOfBirth)
                .NotEmpty()
                .Must(BeAValidDateOfBirth)
                .WithMessage("Customer must be at least 18 years old and under 150.");

            RuleFor(x => x.profilePhoto)
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Profile photo must be a valid URL.");
        }

        #region Helper Methods (Domain Bridges)

        private bool BeAValidEmail(string email) => Try(() => Email.From(email));
        private bool BeAValidPassword(string password) => Try(() => Password.From(password));
        private bool BeAValidFirstOrLastName(string name) => Try(() => Name.FromStrict(name));
        private bool BeAValidUserName(string name) => Try(() => Name.From(name));
        private bool BeAValidAddress(string address) => Try(() => Address.Create(address));

        // التعديل هنا: نستخدم ميثود From الجديدة التي تستقبل DateOnly
        private bool BeAValidDateOfBirth(string date) => Try(() => DateOfBirth.From(date));

        private bool Try(Action action)
        {
            try { action(); return true; }
            catch { return false; }
        }

        #endregion
    }
}