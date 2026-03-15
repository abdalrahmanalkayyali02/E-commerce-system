using System;
using System.Collections.Generic;

namespace ECommerce.Application.DTO.Catalog.Catogry.Response
{
    public record GetAllCatogryResponse(List<CategoryItemDTO> Categories);

    public record CategoryItemDTO(
        Guid CategoryID,
        string CategoryName,
        Guid? ParentCategoryID 
    );
}