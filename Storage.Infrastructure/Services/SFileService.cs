using Storage.Domain.Entities;
using Storage.Domain.Interfaces;
using Storage.Domain.Models;
using Storage.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Services
{
    public class SFileService : ISFileService
    {
        private readonly ISFileRepository _sfileRepository;

        public SFileService(ISFileRepository sfileRepository)
        {
            _sfileRepository = sfileRepository;
        }

        public async Task<SFile> GetSFileById(int id)
        {
            return await _sfileRepository.GetSFileById(id);
        }

        public async Task<PagedData<SFile>> GetSFileList(int pageIndex , int pageSize)
        {
            return await _sfileRepository.GetSFileList(pageIndex, pageSize);
        }

        public async Task UploadSFile(SFile sfile)
        {
            await _sfileRepository.UploadSFile(sfile);
        }

        public async Task UpdateSFile(SFile sfile)
        {
            await _sfileRepository.UpdateSFile(sfile);
        }

        public async Task DeleteSFile(int id)
        {
            await _sfileRepository.DeleteSFile(id);
        }
    }
}
