using FluentValidation;
using System.Linq;

namespace ECommerce.Application.Feature.ShopMangement.shop.updateShopDetails.Command
{
    public class UpdateShopDetailCommandValidator : AbstractValidator<UpdateShopDetailCommand>
    {
        public UpdateShopDetailCommandValidator()
        {
            RuleFor(x => x.shopName)
                .MaximumLength(20).WithMessage("Shop name cannot exceed 20 characters.")
                .Must(name => string.IsNullOrEmpty(name) || name.Length >= 3)
                .WithMessage("If provided, shop name must be at least 3 characters.");

            RuleFor(x => x.shopPhoto)
                .Must(BeAValidStream).WithMessage("Invalid photo stream.")
                .When(x => x.shopPhoto != null);

            RuleFor(x => x.verfiedSellerDocument)
                .NotNull().WithMessage("Seller verification document is required.")
                .Must(BeAValidStream).WithMessage("Seller document stream is empty.");

            RuleFor(x => x.verfiedShopDocument)
                .NotNull().WithMessage("Shop verification document is required.")
                .Must(BeAValidStream).WithMessage("Shop document stream is empty.");
        }

        private bool BeAValidStream(Stream? stream)
        {
            return stream != null && stream.CanRead && stream.Length > 0;
        }
    }
}