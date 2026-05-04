using WebApplication1.Service.IExternalService.Abstraction;

namespace WebApplication1.Service.IExternalService.Impl;

public class BCryptPasswordService : IPasswordService
{
    public  string PasswordHash(string password)
    {
        return  BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool PasswordVerify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}