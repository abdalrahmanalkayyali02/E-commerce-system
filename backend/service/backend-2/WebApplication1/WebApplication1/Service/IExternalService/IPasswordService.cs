namespace WebApplication1.Service.IExternalService;

    public interface IPasswordService
    {
        public string PasswordHash(string password);  
        public bool PasswordVerify(string password, string hash);
    }