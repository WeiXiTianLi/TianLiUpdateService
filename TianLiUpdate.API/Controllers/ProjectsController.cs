using Microsoft.AspNetCore.Mvc;
using TianLiUpdate.API.Data;
using TianLiUpdate.API.Models;

namespace TianLiUpdate.API.Controllers
{
    [Route("")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public ProjectsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Projects/Names
        [HttpGet("Projects/Names")]
        public ActionResult<IEnumerable<string>> GetProjectsName()
        {
            return Ok(_context.Projects.Select(p => p.Name));
        }

        // POST Projects
        [HttpPost]
        public IActionResult PostProject(string name, string token)
        {
            var tokens = _context.Tokens.Where(t => t.TokenString == token);
            if (tokens.Count() == 0)
            {
                return Unauthorized("Token Unauthorized");
            }
            _context.Projects.Add(new ProjectItem { Name = name, CreateTokenId = tokens.First().TokenID });
            _context.SaveChanges();
            return Ok();
        }
        // POST: ProjectName/Version
        [HttpPost("{name}/Version")]
        public IActionResult PostVersion(string name, string token, [FromBody] ProjectVersion version)
        {
            var tokens = _context.Tokens
            .Where(t => t.TokenString == token);
            if (tokens.Count() == 0)
            {
                return Unauthorized("Token Unauthorized");
            }
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            //if(project.VersionIds == null)
            var versions = _context.Versions
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .Where(v => v.Version == version.Version);
            if (versions.Count() != 0)
            {
                return BadRequest("Version already exists");
            }

            version.ProjectVersionID = Guid.NewGuid();
            version.CreateTime = DateTime.Now;
            version.Create_TokenId = tokens.First().TokenID;
            version.ProjectItemID = project.ProjectItemID;
            _context.Versions.Add(version);
            _context.SaveChanges();

            return Ok(version);
        }

        // GET: ProjectName
        [HttpGet("{name}")]
        public IActionResult GetVersionJson(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            var versions = _context.Versions.ToList();
            if (versions == null)
            {
                return NotFound("No versions found");
            }

            var version = versions
                .Where(v => v.ProjectItemID == project.ProjectItemID)
                .OrderByDescending(v => v.CreateTime)
                .FirstOrDefault();

            if (version == null)
            {
                return NotFound("No version found");
            }
            return Ok(new
            {
                version = version.Version,
                downloadUrl = version.DownloadUrl,
                hash = version.Hash
            });
        }
        // GET: ProjectName/List
        [HttpGet("{name}/Versions")]
        public IActionResult GetVersionLists(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            var versions = _context.Versions.ToList();
            if (versions == null)
            {
                return NotFound("No versions found");
            }

            var version = versions
                .Where(v => v.ProjectItemID == project.ProjectItemID)
                .OrderByDescending(v => v.CreateTime);

            if (version == null)
            {
                return NotFound("No version found");
            }
            return Ok(version.Select(v => new
            {
                version = v.Version,
                downloadUrl = v.DownloadUrl,
                hash = v.Hash
            }));
        }
        // GET: ProjectName/Version
        [HttpGet("{name}/Version")]
        public IActionResult GetVersion(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            var versions = _context.Versions.ToList();
            if (versions == null)
            {
                return NotFound("No versions found");
            }

            return Ok(versions
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Version)
            .FirstOrDefault());
        }
        // GET: ProjectName/DownloadUrl
        [HttpGet("{name}/DownloadUrl")]
        public IActionResult GetVersionDownloadUrl(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            return Ok(_context.Versions.ToList()
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.DownloadUrl)
            .FirstOrDefault());
        }
        // GET: ProjectName/Hash
        [HttpGet("{name}/Hash")]
        public IActionResult GetVersionHash(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            return Ok(_context.Versions.ToList()
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Hash)
            .FirstOrDefault());
        }
        // GET: ProjectName/DownloadUrlAndHash
        [HttpGet("{name}/DownloadUrlAndHash")]
        public IActionResult GetVersionDownloadUrlAndHash(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            return Ok(_context.Versions.ToList()
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Hash + "|" + v.DownloadUrl)
            .FirstOrDefault());
        }
        // GET: ProjectName/DependFiles
        [HttpGet("{name}/DependFiles")]
        public IActionResult GetVersionDependFiles(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            var files = _context.Files.ToList();
            var versions = _context.Versions.ToList();
            if (versions == null)
            {
                return NotFound("No versions found");
            }

            return Ok(versions
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Files)
            .Select(fs => fs.Select(f => new
            {
                fileName = f.FileName,
                filePath = f.FilePath,
                downloadUrl = f.DownloadUrl,
                hash = f.Hash
            }))
            .FirstOrDefault());
        }

        // GET: ProjectName/DependFiles
        [HttpGet("{name}/DependFilesDownloadUrlAndHash")]
        public IActionResult GetVersionDependFilesDownloadUrlAndHash(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            var files = _context.Files.ToList();
            var versions = _context.Versions.ToList();
            if (versions == null)
            {
                return NotFound("No versions found");
            }

            return Ok(versions
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Files)
            .Select(fs => string.Join("\n", fs.Select(f => f.Hash + "|" + f.DownloadUrl).ToArray()))
            .FirstOrDefault());
        }

        // DELETE: ProjectName/Version
        [HttpDelete("{name}/Version")]
        public IActionResult DeleteVersion(string name, string token, string version)
        {
            var tokens = _context.Tokens
            .Where(t => t.TokenString == token);
            if (tokens.Count() == 0)
            {
                return Unauthorized("Token Unauthorized");
            }
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            var versions = _context.Versions.ToList()
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .Where(v => v.Version == version);
            if (versions.Count() == 0)
            {
                return NotFound("No version found");
            }
            _context.Versions.RemoveRange(versions);
            _context.SaveChanges();
            return Ok();
        }
    }
}
