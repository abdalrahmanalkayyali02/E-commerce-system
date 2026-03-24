using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.Value_Object;
using System;

namespace ECommerce.Domain.modules.Catalog.Entity
{
    public class ProductPhotoEntity
    {
        public Guid Id { get; private set; }
        public Guid productID { get; private set; }

        public PhotoUrl Url { get; private set; }
        public AltText AltText { get; private set; }
        public bool IsMain { get; private set; }

        // Audit & Soft Delete
        public bool IsDelete { get; private set; } = false;
        public DateTime CreateAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; private set; }
        public DateTime? DeleteAt { get; private set; }

        private ProductPhotoEntity() { }

        public ProductPhotoEntity(Guid id, PhotoUrl url, AltText altText, bool isMain)
        {
            Id = id;
            Url = url;
            AltText = altText;
            IsMain = isMain;
            UpdateAt = DateTime.UtcNow;
        }

        public static Result<ProductPhotoEntity> Create(Guid id, string url, string altText, bool isMain)
        {
            var urlResult = PhotoUrl.Create(url);
            if (urlResult.IsError) return urlResult.Errors;

            var altResult = AltText.Create(altText);
            if (altResult.IsError) return altResult.Errors;


            return Result<ProductPhotoEntity>.Success(
                new ProductPhotoEntity(id, urlResult.Value, altResult.Value, isMain));
        }

        // --- Domain Behaviors ---

        public Result<bool> UpdateUrl(string newUrl)
        {
            var result = PhotoUrl.Create(newUrl);
            if (result.IsError) return result.Errors;

            Url = result.Value;
            UpdateAt = DateTime.UtcNow;
            return Result<bool>.Success(true);
        }

        public Result<bool> UpdateAltText(string newAltText)
        {
            var result = AltText.Create(newAltText);
            if (result.IsError) return result.Errors;

            AltText = result.Value;
            UpdateAt = DateTime.UtcNow;
            return Result<bool>.Success(true);
        }

        public void MarkAsMain()
        {
            IsMain = true;
            UpdateAt = DateTime.UtcNow;
        }

        public void UnmarkAsMain()
        {
            IsMain = false;
            UpdateAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            IsDelete = true;
            DeleteAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }
    }
}