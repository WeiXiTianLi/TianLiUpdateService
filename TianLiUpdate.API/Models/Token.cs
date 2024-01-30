using System.ComponentModel.DataAnnotations;

namespace TianLiUpdate.API.Models
{
    public record Token
    {
        [Key]
        [Required]
        public required Guid TokenId { get; set; } = Guid.Empty;
        [Required]
        public required string TokenString { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public required DateTime LastUseTime { get; set; } = DateTime.Now;
    }
}