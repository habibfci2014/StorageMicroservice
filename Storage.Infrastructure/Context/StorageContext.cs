using Microsoft.EntityFrameworkCore;
using Storage.Domain.Entities;

namespace Storage.Infrastructure.Context
{
    public class StorageContext : DbContext
    {
        public StorageContext(DbContextOptions<StorageContext> options) : base(options) { }

        public DbSet<SFile> SFile { get; set; }
    }
}

