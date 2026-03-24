using System.IO;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstraction.IExternalService
{
    public interface IFileStorgeService
    {
        Task<string> UploadAsync(Stream fileStream);

        Task<string> UpdateAsync(Stream file, string oldFileUrl);

        Task DeleteAsync(string fileUrl);
    }
}