using Application;
using Application.CustomExceptions;
using Application.Persons;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Persistence
{
    public class FileService : IFileService
    {
        private readonly string _storagePath;

        public FileService(IConfiguration configuration, IHostingEnvironment environment)
        {
            _storagePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, configuration["FileStorageSettings:StoragePath"]));
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

            using (var fileStream = new FileStream(Path.Combine(_storagePath, file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return _storagePath;
        }
    }
}