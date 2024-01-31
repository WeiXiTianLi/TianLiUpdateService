namespace TianLiUpdate.API.Models
{
    public record Token
    {
        public Guid Id { get; set; }
        public string TokenString { get; set; }
        public DateTime LastUseTime { get; set; }
    }
}