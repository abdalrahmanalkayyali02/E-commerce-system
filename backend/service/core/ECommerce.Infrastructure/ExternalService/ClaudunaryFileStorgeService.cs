using ECommerce.Application.Abstraction.IExternalService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.ExternalService
{
    public class ClaudunaryFileStorgeService : IFileStorgeService
    {
        public Task DeleteAsync(string fileUrl)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(Stream file, string oldFileUrl)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadAsync(Stream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}
