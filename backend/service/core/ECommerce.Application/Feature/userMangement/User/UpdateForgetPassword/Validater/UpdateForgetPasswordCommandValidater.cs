using ECommerce.Application.Feature.userMangement.User.UpdateForgetPassword.Command;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using FluentValidation;

public class UpdateForgetPasswordCommandValidater : AbstractValidator<updateForgetPasswordCommand>
{
    public UpdateForgetPasswordCommandValidater()
    {
        RuleFor(x => x.password)
            .NotEmpty().WithMessage("Password is required.")
            .Must(pass => !Password.From(pass).IsError)
            .WithMessage("Invalid password format.");

    }
}