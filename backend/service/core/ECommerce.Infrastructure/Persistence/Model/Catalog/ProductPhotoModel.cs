using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Model.Catalog
{
    public class ProductPhotoModel
    {
        public Guid id { get; set; }
        public string url { get; set; }
        public string alterText { get; set; }
        public bool isMain { get; set; }
        public bool isDelete { get; set; }
        public DateTime CreateAt { get; set; } 
        public DateTime UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }

        public Guid ProductId { get; set; }

    }
}
