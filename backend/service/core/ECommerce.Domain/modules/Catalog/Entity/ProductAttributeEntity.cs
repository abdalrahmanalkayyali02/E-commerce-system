using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.DomainError;
using ECommerce.Domain.modules.Catalog.Value_Object;
using System;

namespace ECommerce.Domain.modules.Catalog.Entity
{
    public class ProductAttributeEntity
    {
        public Guid ProductAttributeID { get; private set; }
        public Guid productID { get; private set; }

        public AttributeName AttributeName { get; private set; }
        public AttributeValue AttributeValue { get; private set; }
        public Unit? Unit { get; private set; }
        public bool IsFilterable { get; private set; } = false;
        public bool IsVariant { get; private set; } = false;
        public int DisplayOrder { get; private set; } = 1;


        // Audit Fields
        public bool IsDelete { get; private set; } = false;
        public DateTime? DeleteAt { get; private set; } = null;
        public DateTime CreateAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; private set; }

        private ProductAttributeEntity() { }

        public ProductAttributeEntity(
            Guid id,
            AttributeName name,
            AttributeValue value,
            bool isFilterable,
            bool isVariant,
            int displayOrder,
            Unit? unit)
        {
            ProductAttributeID = id;
            AttributeName = name;
            AttributeValue = value;
            IsFilterable = isFilterable;
            IsVariant = isVariant;
            DisplayOrder = displayOrder;
            Unit = unit;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
            IsDelete = false;
        }

        public static Result<ProductAttributeEntity> Create(
            Guid id,
            string name,
            string value,
            bool isFilterable,
            bool isVariant,
            int displayOrder,
            string? unitValue)
        {
            var nameResult = AttributeName.Create(name);
            if (nameResult.IsError) return nameResult.Errors;

            var valueResult = AttributeValue.Create(value);
            if (valueResult.IsError) return valueResult.Errors;

            Unit? unit = null;
            if (!string.IsNullOrWhiteSpace(unitValue))
            {
                var unitResult = Unit.Create(unitValue);
                if (unitResult.IsError) return unitResult.Errors;
                unit = unitResult.Value;
            }

            return Result<ProductAttributeEntity>.Success(
                new ProductAttributeEntity(id, nameResult.Value, valueResult.Value, isFilterable, isVariant, displayOrder, unit));
        }


        public void UpdateDisplayOrder(int newOrder)
        {
            if (newOrder < 0) return; // Basic guard
            DisplayOrder = newOrder;
            UpdateAt = DateTime.UtcNow;
        }

        public void MarkAsFilterable()
        {
            IsFilterable = true;
            UpdateAt = DateTime.UtcNow;
        }

        public void MarkAsNotFilterable()
        {
            IsFilterable = false;
            UpdateAt = DateTime.UtcNow;
        }

        public void MarkAsVariant()
        {
            IsVariant = true;
            UpdateAt = DateTime.UtcNow;
        }

        public void MarkAsNotVariant()
        {
            IsVariant = false;
            UpdateAt = DateTime.UtcNow;
        }

        public Result<bool> UpdateDetails(string name, string value, string? unitValue)
        {
            var nameResult = AttributeName.Create(name);
            if (nameResult.IsError) return nameResult.Errors;

            var valueResult = AttributeValue.Create(value);
            if (valueResult.IsError) return valueResult.Errors;

            Unit? unit = null;
            if (!string.IsNullOrWhiteSpace(unitValue))
            {
                var unitResult = Unit.Create(unitValue);
                if (unitResult.IsError) return unitResult.Errors;
                unit = unitResult.Value;
            }

            AttributeName = nameResult.Value;
            AttributeValue = valueResult.Value;
            Unit = unit;
            UpdateAt = DateTime.UtcNow;

            return Result<bool>.Success(true);
        }

        public void Delete()
        {
            IsDelete = true;
            DeleteAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }
    }
}