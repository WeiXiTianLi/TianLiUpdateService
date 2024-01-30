using System.ComponentModel.DataAnnotations;

namespace TianLiUpdate.API.Models
{
    public record File
    {
        [Key]
        [Required]
        public required Guid Id { get; set; } = Guid.Empty;
        [Required]
        public required string Hash { get; set; } = string.Empty;
        [Required]
        public required string FileName { get; set; } = string.Empty;
        [Required]
        public required string FilePath { get; set; } = string.Empty;
        [Required]
        public required string DownloadUrl { get; set; } = string.Empty;

        // Foreign Key
        [Required]
        public required ProjectVersion ProjectVersion { get; set; }
    }
}