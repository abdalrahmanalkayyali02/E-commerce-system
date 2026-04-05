using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ECommerce.Application.Abstraction.IExternalService;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Infrastructure.ExternalService
{
    public class ClaudunaryFileStorgeService : IFileStorgeService
    {
        private readonly Cloudinary _cloudinary;
        private const string FolderName = "ecommerce_profile_photos";

        public ClaudunaryFileStorgeService(IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true; 
        }

        public async Task<string> UploadAsync(Stream fileStream)
        {
            var fileName = Guid.NewGuid().ToString();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, fileStream),
                AssetFolder = FolderName,
                PublicId = fileName, 

                Transformation = new Transformation()
                    .Width(500)
                    .Height(500)
                    .Crop("fill")
                    .Gravity("face") 
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception($"Cloudinary Upload Failed: {uploadResult.Error.Message}");
            }

            return uploadResult.SecureUrl.ToString();
        }

        public async Task<string> UpdateAsync(Stream file, string oldFileUrl)
        {
            if (!string.IsNullOrEmpty(oldFileUrl))
            {
                await DeleteAsync(oldFileUrl);
            }

            return await UploadAsync(file);
        }

        public async Task DeleteAsync(string fileUrl)
        {
            var publicId = ExtractPublicIdFromUrl(fileUrl);

            if (string.IsNullOrEmpty(publicId)) return;

            var deletionParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deletionParams);
        }

        private string? ExtractPublicIdFromUrl(string fileUrl)
        {
            try
            {
                var uri = new Uri(fileUrl);
                var segments = uri.AbsolutePath.Split('/');

                var fileNameWithExtension = segments.Last();

                var fileName = Path.GetFileNameWithoutExtension(fileNameWithExtension);

                return $"{FolderName}/{fileName}";
            }
            catch
            {
                return null;
            }
        }
    }
}