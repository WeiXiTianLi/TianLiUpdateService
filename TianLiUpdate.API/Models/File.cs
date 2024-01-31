namespace TianLiUpdate.API.Models
{
    public record File
    {
        public Guid Id { get; set; }
        public string Hash { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string DownloadUrl { get; set; }
        public ProjectVersion ProjectVersion { get; set; }
    }
}