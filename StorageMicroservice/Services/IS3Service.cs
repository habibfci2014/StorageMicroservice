using StorageMicroservice.Model;

namespace StorageMicroservice.Services
{
    public interface IS3Service
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
    }
}
