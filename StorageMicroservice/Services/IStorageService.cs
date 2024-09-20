using StorageMicroservice.Model;

namespace StorageMicroservice.Services
{
    public interface IStorageService
    {
        Task<SFile> DeleteSFile(int id);
        Task<SFile> GetSFileByIdAsync(int id);
        Task<IEnumerable<SFile>> GetSFileListAsync();
        Task<SFile> UploadSFileAsync(SFile sfile);
    }
}
