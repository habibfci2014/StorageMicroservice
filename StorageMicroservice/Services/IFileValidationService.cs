namespace StorageMicroservice.Services
{
    public interface IFileValidationService
    {
        string ValidateFile(IFormFile file);
    }
}
