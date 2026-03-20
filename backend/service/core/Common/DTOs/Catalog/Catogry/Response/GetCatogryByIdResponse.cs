using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.Catalog.Catogry.Response
{
    // if there is no parent set for null
    public record GetCatogryByIdResponse(Guid catorgyID, string name, Guid? parentId);
    
    
    
}
