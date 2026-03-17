using ECommerce.Application.Feature.IAC.User.Create.Command;
using ECommerce.Domain.modules.IAC.ValueObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.IAC.User.Create.Validater
{
    public class CreateSellerValidater : AbstractValidator<CreateSellerCommand>
    {

        #region Helper Method (Domain Bridge)
        private bool BeAValidEmail(string email)
        {
            try { Email.From(email); return true; }
            catch { return false; }
        }

        private bool BeAValidPassword(string password)
        {
            try { Password.From(password); return true; }
            catch { return false; }
        }


        private bool BeAValidFirstOrLastName(string name)
        {
            try
            { Name.FromStrict(name); return true; }
            catch { return false; }
        }

        private bool BeAValidUserName(string name)
        {
            try
            { Name.From(name); return true; }
            catch { return false; }
        }

        private bool BeAValidAddress(string address)
        {
            try { Address.Create(address); return true; }
            catch { return false; }
        }

        private bool BeAValidDateOfBirth(string date)
        {
            try { DateOfBirth.From(date); return true; }
            catch { return false; }
        }

        #endregion

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
                .WithMessage("Password must be at least 8 chars with uppercase,lowercase and digits.");

            RuleFor(x => x.dateOfBirth)
                .NotNull()
                .Must(BeAValidDateOfBirth)
                .WithMessage("Customer must be at least 18 years old.");

            RuleFor(x => x.shopName)
                .NotNull()
                .WithMessage("shop name is require !!")
                .MaximumLength(50)
                .WithMessage("the max length for the shop name is 50 charchter");

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
    }
}
