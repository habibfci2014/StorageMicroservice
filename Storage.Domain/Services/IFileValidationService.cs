using Microsoft.AspNetCore.Http;

namespace Storage.Domain.Services
{
    public interface IFileValidationService
    {
        string ValidateFile(IFormFile file);
    }
}
