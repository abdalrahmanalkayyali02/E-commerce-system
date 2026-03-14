using ECommerce.Application.DTO.IAC.User.Request;
using FluentValidation;
using IAC.Domain.Value_Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.validator.IAC.Registeration
{
    public class LoginValidater : AbstractValidator<LoginUserRequest>
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
