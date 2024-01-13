namespace TianLiUpdate.API.Models
{
    /*
    public enum UpdatePkgMode
    {
        AllPkg,
        NoPkg,
    }
    */
    public class ProjectVersion
    {
        public Guid ProjectVersionID { get; set; } = Guid.Empty;
        public Guid ProjectItemID { get; set; }
        public string Version { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
       // public UpdatePkgMode UpdatePkgMode { get; set; } = UpdatePkgMode.AllPkg;
        //public Tuple<string, string, string> HashFilePaths { get; set; } = new Tuple<string, string, string>(string.Empty, string.Empty, string.Empty);
        //public ICollection<FileHash> Files { get; set; } = new List<FileHash>();
        public string DownloadUrl { get; set; } = string.Empty;
        public ICollection<FileHash> Files { get; set; } = new HashSet<FileHash>();
        public string Hash { get; set; } = string.Empty;
        public string UpdateLog { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public Guid Create_TokenId { get; set; } = Guid.Empty;
        // public Guid ProjectId { get; set; } = Guid.Empty;
        //public ProjectItem Project { get; set; } = new ProjectItem();
    }
}