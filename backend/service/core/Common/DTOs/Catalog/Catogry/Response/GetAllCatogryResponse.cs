using System;
using System.Collections.Generic;

namespace Common.DTOs.Catalog.Catogry.Response
{
    public record GetAllCatogryResponse(List<CategoryItemDTO> Categories);

    public record CategoryItemDTO(
        Guid CategoryID,
        string CategoryName,
        Guid? ParentCategoryID 
    );
}