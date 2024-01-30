using Microsoft.EntityFrameworkCore;
using TianLiUpdate.API.Models;


namespace TianLiUpdate.API.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectVersion> Versions { get; set; }
        public virtual DbSet<Models.File> Files { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
    }
}