using Common.Enum;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("ECommerce.Infrastructure")]

namespace ECommerce.Domain.modules.UserMangement.Entity
{
    public class SellerEntity
    {
        public Guid sellerID { get; private set; }
        public string shopName { get; private set; }
        public string shopPhoto { get; private set; }
        public Address address { get; private set; }
        public bool isVerifiedByAdmin { get; private set; } = false;
        public string verfiedSellerDocument { get; private set; }
        public bool isVerfiedSellerDocumentBeenViewed { get; private set; } = false;
        public string verfiedShopDocument { get; private set; }
        public bool isVerfiedShopDocumentBeenViewed { get; private set; } = false;
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        private SellerEntity() { }

        public SellerEntity(
            Guid id, string shopName, string shopPhoto, Address address, bool isVerfiedByAdmin,
            string verfiedShopDocument, string VerfiedSelllerDocument, bool isShopDocumentView, bool isSellerDocumentView)
        {
            sellerID = id;
            this.shopName = shopName;
            this.shopPhoto = shopPhoto;
            this.address = address;
            isVerifiedByAdmin = isVerfiedByAdmin;
            this.verfiedShopDocument = verfiedShopDocument;
            verfiedSellerDocument = VerfiedSelllerDocument;
            isVerfiedShopDocumentBeenViewed = isShopDocumentView;
            isVerfiedSellerDocumentBeenViewed = isSellerDocumentView;
        }

        public static Result<SellerEntity> Create(
            Guid id, string shopName, string shopPhoto, Address address, bool isVerfiedByAdmin,
            string verfiedShopDocument, string VerfiedSelllerDocument, bool isShopDocumentView, bool isSellerDocumentView)
        {
            if (string.IsNullOrWhiteSpace(shopName))
                return Error.Validation("Seller.InvalidShopName", "Shop name cannot be empty.");

            if (shopName.Length < 5 || shopName.Length > 20)
                return Error.Validation("Seller.InvalidShopName", "Shop name must be between 5-20 characters.");

            if (string.IsNullOrWhiteSpace(shopPhoto))
                return Error.Validation("Seller.InvalidShopPhoto", "Shop photo cannot be empty.");

            if (string.IsNullOrWhiteSpace(verfiedShopDocument))
                return Error.Validation("Seller.InvalidShopDocument", "Shop document cannot be empty.");

            if (string.IsNullOrWhiteSpace(VerfiedSelllerDocument))
                return Error.Validation("Seller.InvalidSellerDocument", "Seller document cannot be empty.");

            var seller = new SellerEntity(
                id, shopName, shopPhoto, address, isVerfiedByAdmin,
                verfiedShopDocument, VerfiedSelllerDocument, isShopDocumentView, isSellerDocumentView);

            seller.CreateAt = DateTime.UtcNow;
            seller.UpdateAt = DateTime.UtcNow;

            return Result<SellerEntity>.Success(seller);
        }

        public Result<Success> updateShopName(string shopName)
        {
            if (string.IsNullOrWhiteSpace(shopName))
                return Error.Validation("Seller.InvalidShopName", "Shop name cannot be empty.");

            if (shopName.Length < 5 || shopName.Length > 20)
                return Error.Validation("Seller.InvalidShopName", "Shop name must be between 5-20 characters.");

            if (shopName == this.shopName)
                return Result<Success>.Success(new Success());

            this.shopName = shopName;
            UpdateAt = DateTime.UtcNow;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> updateShopPhoto(string shopPhoto)
        {
            if (string.IsNullOrWhiteSpace(shopPhoto))
                return Error.Validation("Seller.InvalidShopPhoto", "Shop photo cannot be empty.");

 
            this.shopPhoto = shopPhoto;
            UpdateAt = DateTime.UtcNow;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> updateShopAddress(Address address)
        {
            if (address is null)
                return Error.Validation("Seller.InvalidAddress", "Address cannot be null.");

        

            this.address = address;
            UpdateAt = DateTime.UtcNow;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> UpdateVerifiedSellerDocument(string verifiedSellerDocument)
        {
            if (verfiedSellerDocument == verifiedSellerDocument)
            {
                return Result<Success>.Success(new Success());
            }

            var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
            bool isValidExtension = false;

            foreach (var ext in allowedExtensions)
            {
                if (verifiedSellerDocument.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                {
                    isValidExtension = true;
                    break;
                }
            }

            if (!isValidExtension)
            {
                return Result<Success>.Failure(
                    Error.Validation("Seller.InvalidDocument", "Document must be a PDF or an Image (JPG/PNG).")
                );
            }

            verfiedSellerDocument = verifiedSellerDocument;
            isVerfiedSellerDocumentBeenViewed = false;
            isVerifiedByAdmin = false;
            UpdateAt = DateTime.UtcNow;

            // later maybe will will make even reaise for the admin 

            return Result<Success>.Success(new Success());
        }

        public Result<Success> markAsVerfied()
        {
            if (isVerifiedByAdmin)
                return Error.Conflict("Seller.AlreadyVerified", "Seller is already verified.");

            isVerifiedByAdmin = true;
            UpdateAt = DateTime.UtcNow;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> markAsUnvervied()
        {
            if (!isVerifiedByAdmin)
                return Error.Conflict("Seller.AlreadyUnverified", "Seller is already unverified.");

            isVerifiedByAdmin = false;
            UpdateAt = DateTime.UtcNow;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> markAsViewForSellerDocument()
        {
            if (isVerfiedSellerDocumentBeenViewed)
                return Error.Conflict("Seller.AlreadyViewed", "Seller document already marked as viewed.");

            isVerfiedSellerDocumentBeenViewed = true;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> markAsNotViewForSellerDocument()
        {
            if (!isVerfiedSellerDocumentBeenViewed)
                return Error.Conflict("Seller.AlreadyNotViewed", "Seller document already marked as not viewed.");

            isVerfiedSellerDocumentBeenViewed = false;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> markAsViewForShopDocument()
        {
            if (isVerfiedShopDocumentBeenViewed)
                return Error.Conflict("Seller.AlreadyViewed", "Shop document already marked as viewed.");

            isVerfiedShopDocumentBeenViewed = true;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> markAsNotViewForShopDocument()
        {
            if (isVerfiedShopDocumentBeenViewed == false)
                return Error.Conflict("Seller.AlreadyNotViewed", "Shop document already marked as not viewed.");

            isVerfiedShopDocumentBeenViewed = false;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> updateVerfiedSellerDocument(string verfiedSellerDocument)
        {
            if (isVerifiedByAdmin)
                return Error.Conflict("Seller.AlreadyVerified", "Cannot update verified seller document when the seller is already verified.");

            if (string.IsNullOrWhiteSpace(verfiedSellerDocument))
                return Error.Validation("Seller.InvalidSellerDocument", "Seller document cannot be empty.");

            this.verfiedSellerDocument = verfiedSellerDocument;
            UpdateAt = DateTime.UtcNow;
            return Result<Success>.Success(new Success());
        }

        public Result<Success> updateVerfiedShopDocument(string verfiedShopDocument)
        {
            if (isVerifiedByAdmin)
                return Error.Conflict("Seller.AlreadyVerified", "Cannot update verified shop document when the seller is already verified.");

            if (string.IsNullOrWhiteSpace(verfiedShopDocument))
                return Error.Validation("Seller.InvalidShopDocument", "Shop document cannot be empty.");

            this.verfiedShopDocument = verfiedShopDocument;
            UpdateAt = DateTime.UtcNow;
            return Result<Success>.Success(new Success());
        }
    }
}