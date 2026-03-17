using ECommerce.Application.Abstraction.IExternalService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.ExternalService
{
    public class JWTService : IJWTService
    {
        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public bool ValidateRefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
