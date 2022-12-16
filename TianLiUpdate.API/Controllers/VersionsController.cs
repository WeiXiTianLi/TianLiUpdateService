using Microsoft.AspNetCore.Mvc;
using TianLiUpdate.API.Data;
using TianLiUpdate.API.Models;

namespace TianLiUpdate.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VersionsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public VersionsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/<VersionsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Versions);
        }

        // GET api/<VersionsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.Versions.Find(id));
        }

        // POST api/<VersionsController>
        [HttpPost]
        public IActionResult Post(ProjectVersion value)
        {
            _context.Versions.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // PUT api/<VersionsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProjectVersion value)
        {
            _context.Versions.Update(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // DELETE api/<VersionsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _context.Versions.Remove(_context.Versions.Find(id));
            _context.SaveChanges();
            return Ok();
        }

        // GET: api/<VersionsController>/5/files
        [HttpGet("{id}/files")]
        public IActionResult GetFiles(int id)
        {
            return Ok(_context.Files.Find(id));
        }

        // POST: api/<VersionsController>/5/files
        [HttpPost("{id}/files")]
        public IActionResult PostFiles(int id, FileHash value)
        {
            _context.Files.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // PUT: api/<VersionsController>/5/files
        [HttpPut("{id}/files")]
        public IActionResult PutFiles(int id, FileHash value)
        {
            _context.Files.Update(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // DELETE: api/<VersionsController>/5/files
        [HttpDelete("{id}/files")]
        public IActionResult DeleteFiles(int id)
        {
            _context.Files.Remove(_context.Files.Find(id));
            _context.SaveChanges();
            return Ok();
        }
    }
}
