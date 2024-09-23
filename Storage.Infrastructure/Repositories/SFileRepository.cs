using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Storage.Domain.Entities;
using Storage.Domain.Filters;
using Storage.Domain.Interfaces;
using Storage.Domain.Models;
using Storage.Infrastructure.Context;

namespace Storage.Infrastructure.Repositories
{
    public class SFileRepository : ISFileRepository
    {
        private readonly StorageContext _context;

        public SFileRepository(StorageContext context)
        {
            _context = context;
        }

        public async Task<SFile> GetSFileById(int id)
        {
            return await _context.SFile.FindAsync(id);
        }

        public async Task<PagedData<SFile>> GetSFileList(int pageIndex, int pageSize)
        {
            var validFilter = new PaginationFilter(pageIndex, pageSize);

            var SFileList = await _context.SFile.ToListAsync();

            var totalRecords = SFileList.Count();
            var pagedData = SFileList.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalPages = (totalRecords / (double)validFilter.PageSize);

            return new PagedData<SFile>
            {
                Result = pagedData,
                TotalRecords = totalRecords,
                LastPage = Convert.ToInt32(Math.Ceiling(totalPages)),
                CurrentPage = validFilter.PageNumber
            };

        }

        public async Task UploadSFile(SFile sfile)
        {
            await _context.SFile.AddAsync(sfile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSFile(SFile sfile)
        {
            _context.SFile.Update(sfile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSFile(int id)
        {
            var sfile = await GetSFileById(id);
            _context.SFile.Remove(sfile);
            await _context.SaveChangesAsync();
        }
    }
}

