using ECommerce.Application.Feature.userMangement.User.Login.Command;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Feature.userMangement.User.Login.Validater
{
    public class LoginValidater : AbstractValidator<LoginUserCommand>
    {
        public LoginValidater()
        {
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .Must(BeAValidEmail).WithMessage("Invalid email format.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.");
        }

        private bool BeAValidEmail(string email)
        {
            try { Email.From(email); return true; }
            catch { return false; }
        }
    }
}
