using Catalog.Domain.Entity;

namespace Catalog.Domain.Aggregate
{
    public sealed class ProductAggregate
    {
        public Guid Id { get; private set; }
        public Guid CategoryId { get; private set; } 
        public Guid SellerId { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string BasePrice { get; private set; } 
        public bool IsActive { get; private set; }

        private readonly List<ProductArtibutreEntity> _attributes = new();
        public IReadOnlyCollection<ProductArtibutreEntity> Attributes => _attributes;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        private const int MIN_PHOTOS = 1;
        private const int MAX_PHOTOS = 8;

        private readonly List<ProductPhotoEntity> _photos = new();
        public IReadOnlyCollection<ProductPhotoEntity> Photos => _photos;

        private ProductAggregate() { }

        // Static Factory Method
        public static ProductAggregate Create(
            Guid categoryId,
            Guid sellerId,
            string name,
            string description,
            string basePrice,
            List<ProductPhotoEntity> photos)
        {
            var product = new ProductAggregate
            {
                Id = Guid.CreateVersion7(), 
                CategoryId = categoryId,
                SellerId = sellerId,
                Name = name,
                Description = description,
                BasePrice = basePrice,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            product.EnsureProductPhotoSize(photos);
            product._photos.AddRange(photos);

            return product;
        }

        private void EnsureProductPhotoSize(List<ProductPhotoEntity> photos)
        {
            if (photos == null || photos.Count < MIN_PHOTOS)
                throw new Exception($"The product must have at least {MIN_PHOTOS} photo.");

            if (photos.Count > MAX_PHOTOS)
                throw new Exception($"The product cannot have more than {MAX_PHOTOS} photos.");
        }
    }
}