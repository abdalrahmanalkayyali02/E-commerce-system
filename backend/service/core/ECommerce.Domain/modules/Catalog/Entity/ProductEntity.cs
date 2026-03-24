using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.Value_Object;
using ECommerce.Domain.modules.Catalog.DomainError;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Domain.modules.Catalog.Entity
{
    public sealed class ProductEntity
    {
        public Guid Id { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid SellerId { get; private set; }

        public ProductName Name { get; private set; }
        public ProductDescription Description { get; private set; }
        public decimal BasePrice { get; private set; }
        public bool IsActive { get; private set; }

        private readonly List<ProductAttributeEntity> _attributes = new();
        public IReadOnlyCollection<ProductAttributeEntity> Attributes => _attributes.AsReadOnly();

        private readonly List<ProductPhotoEntity> _photos = new();
        public IReadOnlyCollection<ProductPhotoEntity> Photos => _photos.AsReadOnly();

        // Audit Fields
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; } = null;
        public bool IsDeleted { get; private set; } = false;

        private const int MIN_PHOTOS = 1;
        private const int MAX_PHOTOS = 8;

        private ProductEntity() { }

        public ProductEntity(
            Guid id,
            Guid categoryId,
            Guid sellerId,
            ProductName name,
            ProductDescription description,
            decimal basePrice,
            List<ProductPhotoEntity> photos)
        {
            Id = id;
            CategoryId = categoryId;
            SellerId = sellerId;
            Name = name;
            Description = description;
            BasePrice = basePrice;
            _photos = photos;

            // Initialization
            IsActive = true;
            IsDeleted = false;
            DeletedAt = null;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Result<ProductEntity> Create(
            Guid categoryId,
            Guid sellerId,
            string name,
            string description,
            decimal basePrice,
            List<ProductPhotoEntity> photos)
        {
            var nameResult = ProductName.Create(name);
            if (nameResult.IsError) return nameResult.Errors;

            var descResult = ProductDescription.Create(description);
            if (descResult.IsError) return descResult.Errors;

            if (basePrice <= 0)
                return Result<ProductEntity>.Failure(Error.Validation("Product.InvalidPrice", "Price must be greater than zero."));

            if (photos == null || photos.Count < MIN_PHOTOS)
                return Result<ProductEntity>.Failure(Error.Validation("Product.PhotosRequired", $"Product must have at least {MIN_PHOTOS} photo."));

            if (photos.Count > MAX_PHOTOS)
                return Result<ProductEntity>.Failure(Error.Validation("Product.TooManyPhotos", $"Product cannot have more than {MAX_PHOTOS} photos."));

            if (!photos.Any(p => p.IsMain))
                return Result<ProductEntity>.Failure(Error.Validation("Product.MainPhotoRequired", "One photo must be set as the main photo."));

            return Result<ProductEntity>.Success(
                new ProductEntity(Guid.NewGuid(), categoryId, sellerId, nameResult.Value, descResult.Value, basePrice, photos)
            );
        }

        // --- Domain Behaviors (Soft Delete & Restore) ---

        public void Delete()
        {
            if (IsDeleted) return;

            IsDeleted = true;
            IsActive = false;
            DeletedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            if (!IsDeleted) return;

            IsDeleted = false;
            IsActive = true;
            DeletedAt = null;
            UpdatedAt = DateTime.UtcNow;
        }


        public Result<bool> UpdateDetails(string name, string description, decimal price, Guid categoryId)
        {
            if (IsDeleted) return Result<bool>.Failure(Error.Validation("Product.Deleted", "Cannot update a deleted product."));

            var nameResult = ProductName.Create(name);
            if (nameResult.IsError) return nameResult.Errors;

            var descResult = ProductDescription.Create(description);
            if (descResult.IsError) return descResult.Errors;

            if (price <= 0)
                return Result<bool>.Failure(Error.Validation("Product.InvalidPrice", "Price must be positive."));

            Name = nameResult.Value;
            Description = descResult.Value;
            BasePrice = price;
            CategoryId = categoryId;

            UpdatedAt = DateTime.UtcNow;
            return Result<bool>.Success(true);
        }

        public Result<bool> SetMainPhoto(Guid photoId)
        {
            var photo = _photos.FirstOrDefault(p => p.Id == photoId);
            if (photo == null) return Result<bool>.Failure(Error.NotFound("Product.PhotoNotFound", "Photo not found."));

            foreach (var p in _photos) p.UnmarkAsMain();

            photo.MarkAsMain();
            UpdatedAt = DateTime.UtcNow;
            return Result<bool>.Success(true);
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddAttribute(ProductAttributeEntity attribute)
        {
            if (attribute == null) return;

            if (_attributes.Any(a => a.AttributeName == attribute.AttributeName))
                return;

            _attributes.Add(attribute);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}