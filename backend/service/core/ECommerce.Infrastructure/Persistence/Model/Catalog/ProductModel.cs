using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Model.Catalog
{
    public class ProductModel
    {
        public Guid ProductID { get; set; }
        public Guid CategouryID { get; set; }
        public Guid SellerID { get; set; }
        public string productName { get; set; }
        public string prodcutDescription { get; set; }
        public decimal BasePrice { get; set; }
        public bool isActive { get; set; }
        
        public bool isDeleted { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }

        public virtual ICollection<ProductAttributeModel> ProductAttributes { get; set; } = new List<ProductAttributeModel>();
        public virtual ICollection<ProductPhotoModel> ProductPhotoModels { get; set; } = new List<ProductPhotoModel>();

    }
}

