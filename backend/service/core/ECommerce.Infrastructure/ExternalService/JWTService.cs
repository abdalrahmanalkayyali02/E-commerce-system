using ECommerce.Application.Abstraction.IExternalService;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration; 

namespace ECommerce.Infrastructure.ExternalService
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string userId, string userType)
        {
            var jwtSetting = _configuration.GetSection("JWTSettings");
            var issuer = jwtSetting["Issuer"];
            var audience = jwtSetting["Audience"];
            var secretKey = jwtSetting["SecretKey"];
            var expiryMinutes = int.Parse(jwtSetting["TokenExpirationInMinutes"]!);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              //  new Claim("UserType", userType),
                new Claim(ClaimTypes.Role, userType)
            };
   

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenObj = tokenHandler.CreateToken(descriptor); // for token object 

            // this for response 
            return tokenHandler.WriteToken(tokenObj);
        }

        public string GenerateRefreshToken()
        {
            // Simple approach: Generate a long random string
            return Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks;
        }



    }
}