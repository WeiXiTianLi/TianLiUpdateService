namespace TianLiUpdate.API.Models
{
    public class Token
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string TokenString { get; set; } = string.Empty;
        public DateTime LastUseTime { get; set; } = DateTime.Now;
    }
}