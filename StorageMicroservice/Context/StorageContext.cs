using Microsoft.EntityFrameworkCore;
using StorageMicroservice.Model;

namespace StorageMicroservice.Context
{
    public class StorageContext : DbContext
    {
        public StorageContext(DbContextOptions<StorageContext> options) : base(options) { }

        public DbSet<SFile> SFile { get; set; }
    }
}

