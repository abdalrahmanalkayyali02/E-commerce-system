using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Helper
{
    public class JwtOptions
    {
        public const string SectionName = "JWTSettings";

        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int TokenExpirationInMinutes { get; set; }
        public string SecretKey { get; set; } = string.Empty;
    }
}
