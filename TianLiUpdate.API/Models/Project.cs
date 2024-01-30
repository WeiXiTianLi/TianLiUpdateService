using System.ComponentModel.DataAnnotations;

namespace TianLiUpdate.API.Models
{
    public record Project
    {
        [Key]
        [Required]
        public required Guid Id { get; set; } = Guid.Empty;
        [Required]
        public required string Name { get; set; } = string.Empty;
        [Required]
        public required Guid CreateTokenId { get; set; } = Guid.Empty;
        [Required]
        public HashSet<ProjectVersion> Versions { get; set; } = new();
    }
}