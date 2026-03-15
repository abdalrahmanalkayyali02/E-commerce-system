using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.DTO.Catalog.Catogry.Response
{
    // if there is no parent set for null
    public record GetCatogryByIdResponse(Guid catorgyID, string name, Guid? parentId);
    
    
    
}
