using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Entity
{
    public class ProductPhotoEntity
    {
        public Guid id { get; private set; }
        public string Url { get; private set; }
        public bool isMain { get; private set; }

        public bool isDelete { get; private set; } = false;
        public DateTime CreateAt { get; private set; } = DateTime.Now;
        public DateTime UpdateAt { get; private set; }
        public DateTime DeleteAt { get; private set; }


        public string alterText { get; private set; } = string.Empty;

        public void updateUrl(string Url)
        {
            if (Url is null)
            {
                throw new Exception("the product photo can not be null !!");
            }

            this.Url = Url;
        }

        public void markAsMainPhoto()
        {
            isMain = true;
        }

        public void markAsNotMainPhoto()
        {
            isMain = false; 
        }

        public void updateAlterText(string AlterText)
        {
            alterText = AlterText;
        }


    }
}
