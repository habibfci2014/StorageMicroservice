using Storage.Domain.Entities;
using Storage.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Domain.Services
{
    public interface ISFileService
    {
        Task<SFile> GetSFileById(int id);
        Task<PagedData<SFile>> GetSFileList(int pageIndex, int pageSize);
        Task UploadSFile(SFile sfile);
        Task UpdateSFile(SFile sfile);
        Task DeleteSFile(int id);
    }
}
