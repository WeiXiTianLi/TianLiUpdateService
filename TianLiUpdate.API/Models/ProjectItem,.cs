namespace TianLiUpdate.API.Models
{
    public class ProjectItem
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;  
        public string Description { get; set; } = string.Empty;
        public Token CreateToken { get; set; } = new Token();
        public ICollection<ProjectVersion> Versions { get; set; } = new List<ProjectVersion>();
    }
}