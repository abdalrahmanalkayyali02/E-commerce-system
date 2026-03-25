using Common.Specfication;
using ECommerce.Domain.modules.Catalog.Entity;

namespace ECommerce.Domain.modules.Catalog.Specfication
{
    public class DuplicateProductSpecification : BaseSpecification<ProductEntity>
    {
        public DuplicateProductSpecification(
            Guid sellerId,
            string name,
            string description,
            decimal price)
            : base(p => p.SellerId == sellerId &&
                        p.Name.Value == name &&
                        p.Description.Value == description &&
                        p.BasePrice == price &&
                        !p.IsDeleted)
        {
            AddInclude(p => p.Photos);
            AddInclude(p => p.Attributes);
        }
    }
}