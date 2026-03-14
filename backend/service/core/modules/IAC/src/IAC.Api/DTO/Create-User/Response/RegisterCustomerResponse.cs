using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Application.Contract.Create_User.Response
{
    public record RegisterCustomerResponse(Guid userID,string message);
}
