using Microsoft.AspNetCore.Mvc;
using TianLiUpdate.API.Data;
using TianLiUpdate.API.Models;
using static TianLiUpdate.API.Request.AddRequest;

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

        private IActionResult CheckToken(string token)
        {
#if DEBUG
            return Ok();
#endif
            var tokens = _context.Tokens.Where(t => t.TokenString == token);
            if (tokens.Count() == 0)
            {
                return Unauthorized("Token Unauthorized");
            }
            return Ok();
        }

        // GET: Projects/Names
        [HttpGet("Projects/Names")]
        public ActionResult<IEnumerable<string>> GetProjectsName()
        {
            return Ok(_context.Projects.Select(p => p.Name));
        }

        // POST Projects
        [HttpPost]
        public IActionResult PostProject(AddProjectRequest projectRequest, string token)
        {
            var checkRes = CheckToken(token);
            if (checkRes is UnauthorizedResult)
            {
                return checkRes;
            }

            _context.Projects.Add(new Project
            {
                Id = Guid.NewGuid(),
                Name = projectRequest.ProjectName
            });
            _context.SaveChanges();
            return Ok();
        }
        // POST: ProjectName/Version
        [HttpPost("{projectName}/Version")]
        public IActionResult PostVersion(string projectName, string token, [FromBody] AddVersionRequest versionRequest)
        {
            var checkRes = CheckToken(token);
            if (checkRes is UnauthorizedResult)
            {
                return checkRes;
            }
            var project = _context.Projects
            .Where(p => p.Name == projectName)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }


            var versions = project.Versions
            .Where(v => v.Version == versionRequest.Version);
            if (versions != null && versions.Count() != 0)
            {
                return BadRequest("Version already exists");
            }
            var files = _context.Files.ToList();
            bool isDraft = (bool)(versionRequest.IsDraft == null ? false : versionRequest.IsDraft);

            var projectVersion = new ProjectVersion
            {
                Id = Guid.NewGuid(),
                Version = versionRequest.Version,
                Description = "",
                DownloadUrl = versionRequest.DownloadUrl,
                Hash = versionRequest.Hash,
                UpdateLog = versionRequest.UpdateLog,
                IsDraft = isDraft,
                CreateTime = DateTime.Now,
                Project = project,
            };
            project.Versions.Add(projectVersion);
            //_context.SaveChanges();

            if (versionRequest.Files != null)
            {
                HashSet<Models.File> filesToAdd = new();

                versionRequest.Files.All(f =>
                {
                    filesToAdd.Add(new Models.File
                    {
                        Id = Guid.NewGuid(),
                        FileName = f.FileName,
                        FilePath = f.FilePath,
                        DownloadUrl = f.DownloadUrl,
                        Hash = f.Hash,
                        ProjectVersion = projectVersion
                    });
                    return true;
                });
                projectVersion.Files.IntersectWith(filesToAdd);
                _context.Files.AddRange(filesToAdd);
            }
            _context.Versions.Add(projectVersion);
            _context.Projects.Update(project);
            _context.SaveChanges();

            return Ok(versionRequest);
        }

        // GET: ProjectName
        [HttpGet("{projectName}")]
        public IActionResult GetVersionJson(string projectName)
        {
            var project = _context.Projects
            .Where(p => p.Name == projectName)
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

            var version = project.Versions
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
                hash = version.Hash,
                dependFilesCount = version.Files?.Count()
            });
        }
        // GET: ProjectName/List
        [HttpGet("{projectName}/Versions")]
        public IActionResult GetVersionLists(string projectName)
        {
            var project = _context.Projects
            .Where(p => p.Name == projectName)
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

            var version = project.Versions
                .OrderByDescending(v => v.CreateTime);

            if (version == null)
            {
                return NotFound("No version found");
            }
            return Ok(version.Select(v => new
            {
                version = v.Version,
                downloadUrl = v.DownloadUrl,
                hash = v.Hash,
                dependFilesCount = v.Files?.Count()
            }));
        }
        // GET: ProjectName/Version
        [HttpGet("{projectName}/LatestVersion")]
        public IActionResult GetVersion(string projectName)
        {
            var project = _context.Projects
            .Where(p => p.Name == projectName)
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

            return Ok(project.Versions
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Version)
            .FirstOrDefault());
        }
        // GET: ProjectName/DownloadUrl
        [HttpGet("{projectName}/DownloadUrl")]
        public IActionResult GetVersionDownloadUrl(string projectName)
        {
            var project = _context.Projects
            .Where(p => p.Name == projectName)
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

            return Ok(project.Versions
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.DownloadUrl)
            .FirstOrDefault());
        }
        // GET: ProjectName/Hash
        [HttpGet("{projectName}/Hash")]
        public IActionResult GetVersionHash(string projectName)
        {
            var project = _context.Projects
            .Where(p => p.Name == projectName)
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

            return Ok(project.Versions
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Hash)
            .FirstOrDefault());
        }
        // GET: ProjectName/DownloadUrlAndHash
        [HttpGet("{projectName}/DownloadUrlAndHash")]
        public IActionResult GetVersionDownloadUrlAndHash(string projectName)
        {
            var project = _context.Projects
            .Where(p => p.Name == projectName)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            var versions = _context.Versions.ToList();
            return Ok(project.Versions
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Hash + "|" + v.DownloadUrl)
            .FirstOrDefault());
        }
        // GET: ProjectName/DependFiles
        [HttpGet("{projectName}/DependFiles")]
        public IActionResult GetVersionDependFiles(string projectName)
        {
            var project = _context.Projects
            .Where(p => p.Name == projectName)
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

            var version = project.Versions
                .OrderByDescending(v => v.CreateTime)
                .FirstOrDefault();
            if (version == null)
            {
                return NotFound("No version found");
            }
            if (version.Files == null)
            {
                return NotFound("No files found");
            }
            return Ok(version.Files.Select(f => new
            {
                fileName = f.FileName,
                filePath = f.FilePath,
                downloadUrl = f.DownloadUrl,
                hash = f.Hash
            }));
        }

        // GET: ProjectName/DependFiles
        [HttpGet("{projectName}/DependFilesDownloadUrlAndHash")]
        public IActionResult GetVersionDependFilesDownloadUrlAndHash(string projectName)
        {
            var project = _context.Projects
            .Where(p => p.Name == projectName)
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

            var versionFiles = project.Versions
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Files).FirstOrDefault();

            if (versionFiles == null)
            {
                return Ok();
            }

            return Ok(string.Join("\n", versionFiles.Select(f => f.Hash + "|" + f.DownloadUrl).ToArray()));
        }

        // GET: ProjectName/DependFiles
        [HttpGet("{projectName}/{versionString}/DependFiles")]
        public IActionResult GetOneVersionDependFiles(string projectName, string versionString)
        {
            var project = _context.Projects
            .Where(p => p.Name == projectName)
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


            var version = project.Versions
                .Where(v => v.Version == versionString)
                .FirstOrDefault();
            if (version == null)
            {
                return NotFound("No version found");
            }
            if (version.Files == null)
            {
                return NotFound("No files found");
            }
            return Ok(version.Files.Select(f => new
            {
                fileName = f.FileName,
                filePath = f.FilePath,
                downloadUrl = f.DownloadUrl,
                hash = f.Hash
            }));
        }

        // GET: ProjectName/DependFiles
        [HttpGet("{projectName}/{versionString}/DependFilesDownloadUrlAndHash")]
        public IActionResult GetOneVersionDependFilesDownloadUrlAndHash(string projectName, string versionString)
        {
            var project = _context.Projects
            .Where(p => p.Name == projectName)
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


            var versionFiles = project.Versions
            .Where(v => v.Version == versionString)
            .OrderByDescending(v => v.CreateTime)
            .Select(v => v.Files).FirstOrDefault();

            if (versionFiles == null)
            {
                return Ok();
            }

            return Ok(string.Join("\n", versionFiles.Select(f => f.Hash + "|" + f.DownloadUrl).ToArray()));
        }



        // DELETE: ProjectName/Version
        [HttpDelete("{projectName}/Version")]
        public IActionResult DeleteVersion(string projectName, string token, string versionString)
        {
            var checkRes = CheckToken(token);
            if (checkRes is UnauthorizedResult)
            {
                return checkRes;
            }

            var project = _context.Projects
            .Where(p => p.Name == projectName)
            .FirstOrDefault();
            if (project == null)
            {
                return NotFound("No project found");
            }
            var versions = _context.Versions.ToList();
            var files = _context.Files.ToList();

            var version = project.Versions
            .Where(v => v.Version == versionString);
            if (version == null || version.Count() == 0)
            {
                return NotFound("No version found");
            }
            var versionAllFiles = version.Select(v => v.Files).FirstOrDefault();
            if (versionAllFiles != null && versionAllFiles.Count() != 0)
            {
                foreach (var file in versionAllFiles)
                {
                    _context.Files.Remove(file);
                }
            }
            _context.Versions.RemoveRange(version);
            _context.SaveChanges();
            return Ok();
        }
    }
}
