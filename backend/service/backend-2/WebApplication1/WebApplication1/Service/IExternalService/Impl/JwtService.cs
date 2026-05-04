using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Service.IExternalService.Abstraction;

namespace WebApplication1.Service.IExternalService.Impl;

public class JwtService : ITokenService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
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

        return tokenHandler.WriteToken(tokenObj);
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks;
    }
}