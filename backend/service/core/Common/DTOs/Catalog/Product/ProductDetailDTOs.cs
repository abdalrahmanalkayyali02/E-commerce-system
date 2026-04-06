using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.Catalog.Product
{
    public record ProductDetailDTOs
    (
        Guid productID,
        Guid CategoryId,
        string CategoryName,
        string productName,
        string productDescription,
        decimal basePrice,
        bool isActive        
    );

    // this now for the product photo dto 



    // this now for product detail dto 


    
}
