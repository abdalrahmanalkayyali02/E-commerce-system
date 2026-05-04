using FluentValidation;
using WebApplication1.DTOs;

namespace WebApplication1.Validation;

public class CreateSellerDTOsValidatior : AbstractValidator<CreateSellerDtOs>
{
    public CreateSellerDTOsValidatior()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MinimumLength(2)
            .WithMessage("First name must be at least 2 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MinimumLength(2)
            .WithMessage("Last name must be at least 2 characters long.");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("User name is required.")
            .MinimumLength(3)
            .WithMessage("User name must be at least 3 characters long.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Phone number must be a valid format (E.164 format recommended).");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required.")
            .MinimumLength(5)
            .WithMessage("Address must be at least 5 characters long.");

        // password validation
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.")
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of birth is required.")
            .Must(date => date < DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Date of birth must be in the past.")
            .Must(date => CalculateAge(date) >= 18)
            .WithMessage("You must be at least 18 years old.")
            .Must(date => CalculateAge(date) <= 120)
            .WithMessage("You must be at most 120 years old.");
    }

    private static int CalculateAge(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
