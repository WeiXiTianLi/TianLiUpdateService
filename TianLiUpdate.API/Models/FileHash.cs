namespace TianLiUpdate.API.Models
{
    public class FileHash
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Hash { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string DownloadUrl { get; set; } = string.Empty;
        public ProjectItem Project { get; set; } = new ProjectItem();
    }
}