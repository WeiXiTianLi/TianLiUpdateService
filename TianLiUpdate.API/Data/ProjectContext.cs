using TianLiUpdate.API.Models;
using Microsoft.EntityFrameworkCore;


namespace TianLiUpdate.API.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }
        
        public DbSet<ProjectItem> Projects { get; set; }
        public DbSet<ProjectVersion> Versions { get; set; }
        public DbSet<FileHash> Files { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectItem>().ToTable("Project");
            modelBuilder.Entity<ProjectVersion>().ToTable("Version");
            modelBuilder.Entity<FileHash>().ToTable("File");
            modelBuilder.Entity<Token>().ToTable("Token");
        }
    }
}