namespace Storage.Domain.Services
{
    public interface IS3Service
    {
        Task DeleteFileAsync(string filName);
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
    }
}
