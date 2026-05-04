using FluentValidation;
using WebApplication1.DTOs;

namespace WebApplication1.Validation;

public class UpdateUserProfileDtOsValidatior : AbstractValidator<UpdateUserProfileDtOs>
{
    public UpdateUserProfileDtOsValidatior()
    {
        RuleFor(x => x.FirstName)
            // Fix: Check if the value is provided before applying rules
            .NotEmpty().When(x => x.FirstName != null)
            .WithMessage("First name is required.")
            .MinimumLength(2).When(x => x.FirstName != null)
            .WithMessage("First name must be at least 2 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty().When(x => x.LastName != null)
            .When(x => !string.IsNullOrEmpty(x.LastName))
            .WithMessage("Last name is required.")
            .MinimumLength(2).When(x => x.LastName != null)
            .WithMessage("Last name must be at least 2 characters long.");

        
        RuleFor(x => x.PhoneNumber)
            // Logic: Only validate the format if a phone number was actually sent
            .NotEmpty().When(x => x.PhoneNumber != null)
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Phone Number is required.")
            .MinimumLength(2).When(x => x.PhoneNumber != null)
            .WithMessage("Phone Number must be at least 2 characters long.");

        
        RuleFor(x => x.ProfilePhoto)
            .Must(file => file.Length < 5 * 1024 * 1024) // Example: Limit to 5MB
            .When(x => x.ProfilePhoto != null)
            .WithMessage("Profile photo must be less than 5MB.")
            .Must(file => IsSupportedContentType(file!.ContentType))
            .When(x => x.ProfilePhoto != null)
            .WithMessage("Only JPEG and PNG files are supported.");
    }

    private bool IsSupportedContentType(string contentType)
    {
        return contentType is "image/jpeg" or "image/png" or "image/jpg";
    }
    
}