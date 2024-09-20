using StorageMicroservice.Model;

namespace StorageMicroservice.Services
{
    public interface IS3Service
    {
        Task DeleteFileAsync(string fileName);
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
    }
}
