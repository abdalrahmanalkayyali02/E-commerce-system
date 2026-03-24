using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Model.Catalog
{
    public class ProductAttributeModel
    {
        public Guid ProductAttributeID {  get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public string? Unit { get; set; }
        public int DisplayOrder { get; set; }
        public bool isFilterable { get; set; }
        public bool isVariant { get; set; }
        public bool isDelete { get; set; }
        public DateTime? DeleteAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
