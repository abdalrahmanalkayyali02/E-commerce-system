using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.Catalog
{
    public record ProductAttributeDto(
        string Name,
        string Value,
        string? Unit,
        bool IsFilterable,
        bool IsVariant,
        int DisplayOrder
    );
}
