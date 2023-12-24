using Application;
using Application.CustomExceptions;
using Application.Persons;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public class FileService : IFileService
    {
        private readonly string _storagePath;

        public FileService(IConfiguration configuration)
        {
            _storagePath = configuration["FileStorageSettings:StoragePath"];
        }
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new FileUploadException("File is null");

            if (!ImageValidator.IsAllowedType(file.FileName))
                throw new FileUploadException("File extension type is not allowed");

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }

            var newFileName = Path.GetTempFileName();
            var filePath = Path.Combine(_storagePath, newFileName);

            await file.CopyToAsync(File.Create(filePath));

            return filePath;
        }
    }
}