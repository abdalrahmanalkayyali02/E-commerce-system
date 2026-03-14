using IAC.Application.Abstraction;


namespace IAC.Infrastructure.Gatway
{
    public class CloudinaryGatway : IFileStorgeGatway
    {
        Task IFileStorgeGatway.DeleteAsync(string url)
        {
            throw new NotImplementedException();
        }

        Task<string> IFileStorgeGatway.UpdateAsync(Stream file, string url)
        {
            throw new NotImplementedException();
        }

        Task<string> IFileStorgeGatway.UploadAsync(Stream file)
        {
            throw new NotImplementedException();
        }
    }
}
