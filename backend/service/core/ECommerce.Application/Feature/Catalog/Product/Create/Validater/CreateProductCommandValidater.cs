using ECommerce.Application.Feature.Catalog.Product.Create.Command;
using ECommerce.Domain.modules.Catalog.Value_Object;
using FluentValidation;
using System.Linq;

namespace ECommerce.Application.Feature.Catalog.Product.Create.Validater
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(c => c.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .Must(name => !ProductName.Create(name).IsError)
                .WithMessage("Invalid product name format (Alphanumeric and '&' only).");

            RuleFor(c => c.ProductDescription)
                .NotEmpty().WithMessage("Description is required.")
                .Must(desc => !ProductDescription.Create(desc).IsError)
                .WithMessage("Invalid description format.");

            RuleFor(c => c.BasePrice)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(c => c.SellerId).NotEmpty().WithMessage("Seller ID is required.");
            RuleFor(c => c.CategoryId).NotEmpty().WithMessage("Category ID is required.");

            RuleFor(c => c.ProductPhotos)
                .NotEmpty().WithMessage("At least one photo is required.")
                .Must(p => p != null && p.Count <= 8).WithMessage("Maximum 8 photos allowed.")
                .Must(p => p != null && p.Count(x => x.IsMain) == 1) 
                .WithMessage("Exactly one photo must be set as Main.");

            RuleForEach(c => c.ProductPhotos).ChildRules(photo =>
            {
                photo.RuleFor(x => x.FileStream)
                    .NotNull().WithMessage("File stream is required.")
                    .Must(s => s.Length > 0).WithMessage("File cannot be empty.")
                    .Must(s => s.Length <= 5 * 1024 * 1024).WithMessage("Each photo must be under 5MB.");

                photo.RuleFor(x => x.AltText)
                    .NotEmpty()
                    .Must(alt => !AltText.Create(alt).IsError)
                    .WithMessage("Invalid AltText format.");
            });

            RuleForEach(c => c.ProductAttributes).ChildRules(attr =>
            {
                attr.RuleFor(x => x.Name).NotEmpty();
                attr.RuleFor(x => x.Value).NotEmpty();
                attr.RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
            });
        }
    }
}