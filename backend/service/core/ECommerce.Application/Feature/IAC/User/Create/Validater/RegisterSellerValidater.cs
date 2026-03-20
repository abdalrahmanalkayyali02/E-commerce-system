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
              .Must(BeAValidFirstOrLastName)
              .MaximumLength(50);

            RuleFor(x => x.lastName)
                .NotEmpty()
                .Must(BeAValidFirstOrLastName)
                .MaximumLength(50);

            RuleFor(x => x.userName)
                .NotEmpty()
                .Must(BeAValidUserName)
                .MinimumLength(4);

            RuleFor(x => x.address)
                .NotEmpty()
                .Must(BeAValidAddress);

            RuleFor(x => x.email)
                .NotEmpty()
                .Must(BeAValidEmail)
                .WithMessage("Invalid email format.");

            RuleFor(x => x.password)
                .NotEmpty()
                .Must(BeAValidPassword)
                .WithMessage("Password must be at least 8 chars with uppercase, lowercase and digits.");

            RuleFor(x => x.dateOfBirth)
                .NotNull()
                .Must(BeAValidDateOfBirth)
                .WithMessage("Seller must be at least 18 years old.");

            RuleFor(x => x.shopName)
                .NotEmpty() // تم استبدال NotNull بـ NotEmpty لضمان عدم وجود نص فارغ
                .WithMessage("Shop name is required !!")
                .MaximumLength(50)
                .WithMessage("The max length for the shop name is 50 characters.");

            RuleFor(x => x.profilePhoto)
               .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
               .WithMessage("Profile photo must be a valid URL.");

            RuleFor(x => x.verfiedSellerDocument)
                .NotEmpty().WithMessage("Seller verification document is required.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Seller document must be a valid URL.");

            RuleFor(x => x.verfiedShopDocument)
                .NotEmpty().WithMessage("Shop verification document is required.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Shop document must be a valid URL.");
        }

        #region Helper Method (Domain Bridge)

        // استخدمنا الـ Try ميثود لتقليل تكرار الـ Catch في كل مكان
        private bool Try(Action action)
        {
            try { action(); return true; }
            catch { return false; }
        }

        private bool BeAValidEmail(string email) => Try(() => Email.From(email));

        private bool BeAValidPassword(string password) => Try(() => Password.From(password));

        private bool BeAValidFirstOrLastName(string name) => Try(() => Name.FromStrict(name));

        private bool BeAValidUserName(string name) => Try(() => Name.From(name));

        private bool BeAValidAddress(string address) => Try(() => Address.Create(address));

        // تصحيح الـ Variable Name من data إلى date
        private bool BeAValidDateOfBirth(string date) => Try(() => DateOfBirth.From(date));

        #endregion
    }
}