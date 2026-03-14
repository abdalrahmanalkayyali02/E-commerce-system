using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Entity
{
    public class ProductCatogryEntity
    {
        public Guid CatogryID { get; private set; }
        public string catogryName { get; private set; }
        public string catogryDescription { get; private set; }

        // the parent is product catogry 
        public Guid  parentCategoryID { get; private set; }
        
        // need to addeed 
        public bool isDelete { get; private set; } = false;
        public DateTime CreateAT { get; private set; } = DateTime.Now;
        public DateTime UpdateAT { get; private set; }
        public DateTime DeleteAt { get; private set; }


        private ProductCatogryEntity() { }
        private ProductCatogryEntity(Guid id ,string name, string description, Guid parentCategory)
        {
            CatogryID = id;
            catogryName = name;
            catogryDescription = description;
            this.parentCategoryID = parentCategory;
        }

        public static ProductCatogryEntity Create(Guid id, string name, string description, Guid parentCategory)
        {
            var catogry = new ProductCatogryEntity(id,name,description,parentCategory);

            return catogry;
        }

        public void updateCatogryName(string name)
        {
            catogryName = name;
        }

        public void updateCatogryDescription(string description)
        {
            catogryDescription = description;
        }

        public void updateCatogryParent(Guid parentCategory)
        {
            this.parentCategoryID = parentCategory;
        }

    }
}
