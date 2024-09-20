using System.ComponentModel.DataAnnotations;

namespace StorageMicroservice.Model
{
    public class SFile
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
