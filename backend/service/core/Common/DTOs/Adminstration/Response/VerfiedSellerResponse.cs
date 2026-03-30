using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs.Adminstration.Response
{
    public record VerfiedSellerResponse(Guid sellerID, bool sellerVerfiedStatus, string message);
    
    
}
