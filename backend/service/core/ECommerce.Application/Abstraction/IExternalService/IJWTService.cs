using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Abstraction.IExternalService
{
    public interface IJWTService
    {
        public string GenerateToken(string userId, string role);
        public string GenerateRefreshToken();

    }
}
