using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Entity
{
    public class ProductAttributeEntity
    {
        public Guid ProductAttributeID { get; private set; }
        public string AttributeName { get; private set; }
        public string AttributeValue { get; private set; }
        public bool IsFilterable { get; private set; }

        public bool IsVariant { get; private set; }
        public int DisplayOrder { get; private set; } = 1;

        public string Unit { get; private set; }

        public bool isDelete { get; private set; } = false;
        public DateTime DeleteAt { get; private set; }
        public DateTime CreateAt { get; private set; } = DateTime.Now;
        public DateTime UpdateAt { get; private set; }

        private ProductAttributeEntity() { }

        private ProductAttributeEntity(Guid id, string name, string value, bool isFilterable, bool isVariant, int DisplayOrder, string Unit)
        {
            ProductAttributeID = id;
            AttributeName = name;
            AttributeValue = value;
            IsFilterable = isFilterable;
            IsVariant = isVariant;
            this.DisplayOrder = DisplayOrder;
        }

        public static ProductAttributeEntity Create(Guid id, string name, string value, bool isFilterable, bool isVariant, int DisplayOrder, string Unit)
        {
            var productAttribute = new ProductAttributeEntity();

            return productAttribute;
        }

    }
}
