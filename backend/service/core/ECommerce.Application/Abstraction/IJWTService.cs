using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Application.Abstraction
{
    public interface IJWTService
    {
        public string GenerateToken(string userId, string role);
        public bool ValidateToken(string token);
        public string GenerateRefreshToken();
        public bool ValidateRefreshToken(string token);

    }
}
