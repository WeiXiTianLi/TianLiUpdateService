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
        public Guid Id { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string DownloadUrl { get; set; }
        public HashSet<File>? Files { get; } = new();
        public string Hash { get; set; }
        public string UpdateLog { get; set; }
        public bool IsDraft { get; set; }
        public DateTime CreateTime { get; set; }

        public Project Project { get; set; }
    }
}