namespace TianLiUpdate.API.Models
{
    public class FileHash
    {
        public Guid FileHashID { get; set; } = Guid.Empty;
        public string Hash { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string DownloadUrl { get; set; } = string.Empty;
        //public Project Project { get; set; } = new Project();
    }
}