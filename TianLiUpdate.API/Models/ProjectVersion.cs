namespace TianLiUpdate.API.Models
{
    public enum UpdatePkgMode
    {
        AllPkg,
        NoPkg,
    }
    public class ProjectVersion
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Version { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public UpdatePkgMode UpdatePkgMode { get; set; } = UpdatePkgMode.AllPkg;
        //public Tuple<string, string, string> HashFilePaths { get; set; } = new Tuple<string, string, string>(string.Empty, string.Empty, string.Empty);
        public ICollection<FileHash> Files { get; set; } = new List<FileHash>();
        public string? DownloadUrl { get; set; } = string.Empty;
        public string UpdateLog { get; set; } = string.Empty;
        public Token CreateToken { get; set; } = new Token();
        public ProjectItem Project { get; set; } = new ProjectItem();
    }
}