namespace WEbApplication1.Service.IExternalService;
    public interface IFileStorageService
    {
        Task<string> UploadAsync(Stream fileStream);

        Task<string> UpdateAsync(Stream file, string oldFileUrl);

        Task DeleteAsync(string fileUrl);
    }