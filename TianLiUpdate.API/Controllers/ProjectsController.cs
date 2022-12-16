using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TianLiUpdate.API.Data;
using TianLiUpdate.API.Models;

namespace TianLiUpdate.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(ProjectContext context, ILogger<ProjectsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Projects/Names
        [HttpGet("Names")]
        public  ActionResult<IEnumerable<string>> GetName()
        {
            return Ok(_context.Projects.Select(p => p.Name));
        }
        /*
        // GET Projects/Name
        [HttpGet("{name}")]
        public ActionResult<ProjectItem> Get(string name, string token)
        {
            var tokens = _context.Tokens.Where(t => t.TokenString == token);
            if(tokens.Count() == 0)
            {
                _logger.LogInformation("Token Unauthorized");
                return Unauthorized("Token Unauthorized");
            }
            return Ok(_context.Projects.Where(p => p.Name == name).FirstOrDefault());
        }
        */
        // POST Projects
        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Post(string name, string token)
        {
            var tokens = _context.Tokens.Where(t => t.TokenString == token);
            if(tokens.Count() == 0)
            {
                _logger.LogInformation("Token Unauthorized");
                return Unauthorized("Token Unauthorized");
            }
            _context.Projects.Add(new ProjectItem { Name = name , CreateTokenId = tokens.First().TokenID});
            _context.SaveChanges();
            return Ok();
        }
        /*
        // PUT Projects/Id
        [HttpPut("{id}")]
        public IActionResult Put(int id,  ProjectItem value, string token)
        {
            var tokens = _context.Tokens.Where(t => t.TokenString == token);
            if(tokens.Count() == 0)
            {
                _logger.LogInformation("Token Unauthorized");
                return Unauthorized("Token Unauthorized");
            }
            _context.Projects.Update(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // DELETE Projects/Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, string token)
        {
            var tokens = _context.Tokens.Where(t => t.TokenString == token);
            if(tokens.Count() == 0)
            {
                _logger.LogInformation("Token Unauthorized");
                return Unauthorized("Token Unauthorized");
            }
            var project = _context.Projects.Find(id);
            if(project == null)
            {
                _logger.LogInformation("No project found");
                return NotFound("No project found");
            }
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return Ok();
        }
        */
        // GET: Projects/ProjectName/Versions
        [HttpGet("{name}/Versions")]
        public IActionResult GetVersions(string name)
        {
            var project = _context.Projects
                .Where(p => p.Name == name)
                .FirstOrDefault();
            if(project == null)
            {
                _logger.LogInformation("No project found");
                return NotFound("No project found");
            }
            return Ok(_context.Versions
                .Where(v => v.ProjectItemID == project.ProjectItemID)
                .Select(v => v.Version));
        }

        // GET: Projects/ProjectName/Version
        [HttpGet("{name}/{version}")]
        public IActionResult GetVersion(string name, string version)
        {
            var project = _context.Projects
                .Where(p => p.Name == name)
                .FirstOrDefault();
            if(project == null)
            {
                _logger.LogInformation("No project found");
                return NotFound("No project found");
            }
            if(project.Versions == null)
            {
                _logger.LogInformation("No versions found");
                return NotFound("No versions found");
            }
            var versions = _context.Versions
                .Where(v => v.ProjectItemID == project.ProjectItemID)
                .Where(v => v.Version == version)
                .OrderByDescending(v => v.CreateTime)
                .FirstOrDefault();
            if(versions == null)
            {
                _logger.LogInformation("No version found");
                return NotFound("No version found");
            } 
            return Ok(versions);
        }

        // POST: Projects/ProjectName/Version
        [HttpPost("{name}/Version")]
        public IActionResult PostVersion(string name, string token, [FromBody] ProjectVersion version)
        {
            var tokens = _context.Tokens
            .Where(t => t.TokenString == token);
            if(tokens.Count() == 0)
            {
                _logger.LogInformation("Token Unauthorized");
                return Unauthorized("Token Unauthorized");
            }
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if(project == null)
            {
                _logger.LogInformation("No project found");
                return NotFound("No project found");
            }
            //if(project.VersionIds == null)
            var versions = _context.Versions
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .Where(v => v.Version == version.Version);
            if(versions.Count() != 0)
            {
                _logger.LogInformation("Version already exists");
                return BadRequest("Version already exists");
            }

            version.ProjectVersionID = Guid.NewGuid();
            version.CreateTime = DateTime.Now;
            version.Create_TokenId = tokens.First().TokenID;
            version.ProjectItemID = project.ProjectItemID;
            _context.Versions.Add(version);
            project.Versions.Add(version);
            Console.WriteLine("asdasd"+project.Versions.Count());
            _context.Projects.Update(project);
            _context.SaveChanges();
            Console.WriteLine("asdasdsdasd"+project.Versions.Count());

            return Ok(version);
        }

        // GET: Projects/ProjectName/LatestVersion
        [HttpGet("{name}/LatestVersion")]
        public IActionResult GetLatestVersion(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if(project == null)
            {
                _logger.LogInformation("No project found");
                return NotFound("No project found");
            }
            return Ok(_context.Versions
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Version)
            .FirstOrDefault());
        }

        // GET: Projects/ProjectName/LatestVersion/DownloadUrl
        [HttpGet("{name}/LatestVersion/DownloadUrl")]
        public IActionResult GetLatestVersionDownloadUrl(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if(project == null)
            {
                _logger.LogInformation("No project found");
                return NotFound("No project found");
            }
            return Ok(_context.Versions
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.DownloadUrl)
            .FirstOrDefault());
        }

        // GET: Projects/ProjectName/LatestVersion/Hash
        [HttpGet("{name}/LatestVersion/Hash")]
        public IActionResult GetLatestVersionHash(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if(project == null)
            {
                _logger.LogInformation("No project found");
                return NotFound("No project found");
            }
            return Ok(_context.Versions
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Hash)
            .FirstOrDefault());
        }

        // GET: Projects/ProjectName/LatestVersion/DownloadUrlAndHash
        [HttpGet("{name}/LatestVersion/DownloadUrlAndHash")]
        public IActionResult GetLatestVersionDownloadUrlAndHash(string name)
        {
            var project = _context.Projects
            .Where(p => p.Name == name)
            .FirstOrDefault();
            if(project == null)
            {
                _logger.LogInformation("No project found");
                return NotFound("No project found");
            }
            return Ok(_context.Versions
            .Where(v => v.ProjectItemID == project.ProjectItemID)
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Hash+"|"+v.DownloadUrl)
            .FirstOrDefault());
        }

        // GET: Projects/ProjectName/Version/VersionName/Files
    }
}
