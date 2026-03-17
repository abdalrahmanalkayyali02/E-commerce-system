using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Abstraction.IExternalService
{
    public interface IFileStorgeService
    {
        Task<string> UploadAsync(Stream file);
        Task<string> UpdateAsync(Stream file, string url);
        Task DeleteAsync(string url);
    }
}
