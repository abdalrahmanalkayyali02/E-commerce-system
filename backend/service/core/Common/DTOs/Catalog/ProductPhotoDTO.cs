using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.Catalog
{
    public record ProductPhotoDto(Stream FileStream, string AltText,bool IsMain);
}


