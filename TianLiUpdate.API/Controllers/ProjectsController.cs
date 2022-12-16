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

        // GET: Projects
        [HttpGet]
        public  ActionResult<IEnumerable<ProjectItem>> Get()
        {
            if(_context.Projects == null)
            {
                _logger.LogInformation("No projects found");
                return NotFound();
            }
            return Ok(_context.Projects);
        }
        // GET: Projects/Names
        [HttpGet("Names")]
        public  ActionResult<IEnumerable<string>> GetName()
        {
            if(_context.Projects == null)
            {
                _logger.LogInformation("No projects found");
                return NotFound();
            }
            return Ok(_context.Projects.Select(p => p.Name));
        }

        // GET Projects/Name
        [HttpGet("{name}")]
        public ActionResult<ProjectItem> Get(string name)
        {
            if(_context.Projects == null)
            {
                _logger.LogInformation("No projects found");
                return NotFound();
            }
            return Ok(_context.Projects.Find(name));
        }

        // POST Projects
        [HttpPost]
        public IActionResult Post(ProjectItem value)
        {
            if (_context.Projects == null)
            {
                _logger.LogInformation("No projects found");
                return NotFound();
            }
            _context.Projects.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }
        // POST Projects/Name token
        [HttpPost("{name}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Post(string name)
        {
            if (_context.Projects == null)
            {
                _logger.LogInformation("No projects found");
                return NotFound();
            }
            _context.Projects.Add(new ProjectItem { Name = name });
            _context.SaveChanges();
            return Ok();
        }

        // PUT Projects/Id
        [HttpPut("{id}")]
        public IActionResult Put(int id,  ProjectItem value)
        {
            _context.Projects.Update(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // DELETE Projects/Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _context.Projects.Remove(_context.Projects.Find(id));
            _context.SaveChanges();
            return Ok();
        }

        // GET: api/<ProjectsController>/5/versions
        [HttpGet("{id}/versions")]
        public IActionResult GetVersions(int id)
        {
            return Ok(_context.Versions.Find(id));
        }
    }
}
