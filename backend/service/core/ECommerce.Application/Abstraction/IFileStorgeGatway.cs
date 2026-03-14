using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Application.Abstraction
{
    public interface IFileStorgeGatway
    {
        Task<string> UploadAsync(Stream file);
        Task<string> UpdateAsync(Stream file, string url);
        Task DeleteAsync(string url);
    }
}
