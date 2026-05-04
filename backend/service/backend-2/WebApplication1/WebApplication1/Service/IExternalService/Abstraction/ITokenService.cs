namespace WebApplication1.Service.IExternalService.Abstraction;

public interface ITokenService
{
    public string GenerateToken(string userId, string role);
    public string GenerateRefreshToken();
}