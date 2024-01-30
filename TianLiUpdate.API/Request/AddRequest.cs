using System.ComponentModel.DataAnnotations;

namespace TianLiUpdate.API.Request
{
    public class AddRequest
    {
        public record AddProjectRequest
        {
            [Required] public required string ProjectName { get; set; }
        }

        public record AddFileRequest
        {
            [Required] public required string FileName { get; set; }
            [Required] public required string FilePath { get; set; }
            [Required] public required string DownloadUrl { get; set; }
            [Required] public required string Hash { get; set; }
        }

        public record AddVersionRequest
        {
            [Required] public required string Version { get; set; }
            [Required] public required string DownloadUrl { get; set; }
            [Required] public required string Hash { get; set; }
            [Required] public required string UpdateLog { get; set; }
            public bool? IsDraft { get; set; }
            public HashSet<AddFileRequest>? Files { get; set; }
        }
    }
}
