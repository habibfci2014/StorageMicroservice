using Microsoft.EntityFrameworkCore;
using StorageMicroservice.Context;
using StorageMicroservice.Model;

namespace StorageMicroservice.Services
{
    public class StorageService : IStorageService
    {
        private readonly StorageContext _context;

        public StorageService(StorageContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<SFile>> GetSFileListAsync()
        {
            if (_context.SFile == null)
            {
                return null;
            }
            return await _context.SFile.ToListAsync();
        }

        public async Task<SFile> GetSFileByIdAsync(int id)
        {
            if (_context.SFile == null)
            {
                return null;
            }
            var sfile = await _context.SFile.FindAsync(id);
            return sfile;
        }

        public async Task<SFile> UploadSFileAsync(SFile sfile)
        {
            if (_context.SFile == null)
            {
                return null;
            }
            _context.SFile.Add(sfile);
            await _context.SaveChangesAsync();
            return sfile;
        }

        public async Task<SFile> DeleteSFile(int id)
        {
            if (_context.SFile == null)
            {
                return null;
            }
            var sfile = await _context.SFile.FindAsync(id);
            if (sfile == null)
            {
                return null;
            }

            _context.SFile.Remove(sfile);
            await _context.SaveChangesAsync();

            return sfile;
        }
    }
}
