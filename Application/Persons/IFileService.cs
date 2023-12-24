using Microsoft.AspNetCore.Http;

namespace Application.Persons
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
    }
}