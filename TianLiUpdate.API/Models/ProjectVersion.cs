using System.ComponentModel.DataAnnotations;

namespace TianLiUpdate.API.Models
{
    /*
    public enum UpdatePkgMode
    {
        AllPkg,
        NoPkg,
    }
    */
    public record ProjectVersion
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.Empty;
        [Required]
        public string Version { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string DownloadUrl { get; set; } = string.Empty;
        [Required]
        public HashSet<File> Files { get; } = new();
        [Required]
        public string Hash { get; set; } = string.Empty;
        [Required]
        public string UpdateLog { get; set; } = string.Empty;
        [Required]
        public bool IsDraft { get; set; } = false;
        [Required]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        [Required]
        public Guid CreateTokenId { get; set; } = Guid.Empty;

        [Required]
        public required Project Project { get; set; }
    }
}