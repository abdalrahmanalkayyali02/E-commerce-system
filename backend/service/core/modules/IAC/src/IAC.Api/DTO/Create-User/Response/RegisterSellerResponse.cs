using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Application.DTO.Create_User.Response
{
    public record RegisterSellerResponse(Guid userID, string message);

}
