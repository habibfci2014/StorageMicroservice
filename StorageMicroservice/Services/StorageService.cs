using Microsoft.EntityFrameworkCore;
using StorageMicroservice.Context;
using StorageMicroservice.Model;
using System.Text.RegularExpressions;

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

        public string ValidateFile(IFormFile file)
        {
            long _maxAllowedPosterSize = 4194304;

            if (file.Length > _maxAllowedPosterSize)
                return "Max allowed size for poster is 4MB!";


            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string content = System.Text.Encoding.UTF8.GetString(fileBytes);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy|<php",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return "something wrong, can't upload file";
                }
            }


            return "";
        }
    }
}
