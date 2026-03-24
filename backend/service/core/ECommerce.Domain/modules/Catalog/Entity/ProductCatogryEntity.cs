using Common.Impl.Result;
using Common.Result;
using ECommerce.Domain.modules.Catalog.DomainError;
using ECommerce.Domain.modules.Catalog.Value_Object;
using System;

namespace ECommerce.Domain.modules.Catalog.Entity
{
    public class ProductCategoryEntity
    {
        public Guid CategoryID { get; private set; }
        public CategoryName CategoryName { get; private set; }
        public CategoryDescription CategoryDescription { get; private set; }
        public Guid? ParentCategoryID { get; private set; }

        // Audit Fields
        public bool IsDelete { get; private set; } = false;
        public DateTime CreateAT { get; private set; } = DateTime.UtcNow;
        public DateTime UpdateAT { get; private set; }
        public DateTime? DeleteAt { get; private set; } 

        private ProductCategoryEntity() { }

        public ProductCategoryEntity(Guid id, CategoryName name, CategoryDescription description, Guid? parentCategory)
        {
            CategoryID = id;
            CategoryName = name;
            CategoryDescription = description;
            ParentCategoryID = parentCategory;
            UpdateAT = DateTime.UtcNow;
            IsDelete = false;
        }

        public static Result<ProductCategoryEntity> Create(Guid id, CategoryName name, CategoryDescription description, Guid? parentCategory)
        {
            if (parentCategory.HasValue && parentCategory.Value == id)
            {
                return Result<ProductCategoryEntity>.Failure(Error.Validation
                    ("Category.SelfParent", "A category cannot be its own parent."));
            }

            return Result<ProductCategoryEntity>.Success(
                new ProductCategoryEntity(id, name, description, parentCategory));
        }


        public Result<bool> UpdateName(string newName)
        {
            var nameResult = CategoryName.Create(newName);
            if (nameResult.IsError) return nameResult.Errors;

            CategoryName = nameResult.Value;
            UpdateAT = DateTime.UtcNow;
            return Result<bool>.Success(true);
        }

        public Result<bool> UpdateDescription(string newDescription)
        {
            var descResult = CategoryDescription.Create(newDescription);
            if (descResult.IsError) return descResult.Errors;

            CategoryDescription = descResult.Value;
            UpdateAT = DateTime.UtcNow;
            return Result<bool>.Success(true);
        }

        public Result<bool> UpdateParent(Guid? newParentId)
        {
            if (newParentId.HasValue && newParentId.Value == CategoryID)
                return Result<bool>.Failure(Error.Validation("Category.SelfParent", "A category cannot be its own parent."));

            ParentCategoryID = newParentId;
            UpdateAT = DateTime.UtcNow;
            return Result<bool>.Success(true);
        }

        public void MarkAsDeleted()
        {
            IsDelete = true;
            DeleteAt = DateTime.UtcNow;
            UpdateAT = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsDelete = false;
            DeleteAt = null;
            UpdateAT = DateTime.UtcNow;
        }
    }
}