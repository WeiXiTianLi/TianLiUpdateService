namespace TianLiUpdate.API.Models
{
    public class ProjectItem
    {
        public Guid ProjectItemID { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;  
        public Guid CreateTokenId { get; set; } = Guid.Empty;
        public ICollection<ProjectVersion> Versions { get; set; } = new HashSet<ProjectVersion>();
        //public ICollection<Guid> VersionIds { get; set; } = new List<Guid>();
    }
}