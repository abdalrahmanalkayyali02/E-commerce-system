using FluentValidation;
using WebApplication1.DTOs;

namespace WebApplication1.Validation;

public class LoginDtosValidatior : AbstractValidator<LoginDTos>
{
    public LoginDtosValidatior()
    {
        RuleFor(x=>x.Email)
            .NotEmpty()
            .WithMessage("Email is required.");
        RuleFor(x=>x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
        
    }
}