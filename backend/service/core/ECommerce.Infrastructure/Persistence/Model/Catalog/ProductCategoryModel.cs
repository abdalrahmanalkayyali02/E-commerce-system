using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Model.Catalog
{
    public class ProductCategoryModel
    {
        public Guid CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentCategoryID { get; set; }

        public bool isDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt {  get; set; }
        public DateTime? DeleteAt { get; set; }

        public virtual ProductCategoryModel? ParentCategory { get; set; }
        public virtual ICollection<ProductCategoryModel> SubCategories { get; set; } = new List<ProductCategoryModel>();
        public virtual ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();

    }
}
