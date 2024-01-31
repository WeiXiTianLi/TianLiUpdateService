namespace TianLiUpdate.API.Models
{
    public record Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public HashSet<ProjectVersion>? Versions { get; } = new();
    }
}