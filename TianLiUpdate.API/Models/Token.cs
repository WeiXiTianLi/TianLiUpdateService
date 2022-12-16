namespace TianLiUpdate.API.Models
{
    public class Token
    {
        public Guid TokenID { get; set; } = Guid.Empty;
        public string TokenString { get; set; } = Guid.NewGuid().ToString();
        public DateTime LastUseTime { get; set; } = DateTime.Now;
    }
}