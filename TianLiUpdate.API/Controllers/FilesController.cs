using Microsoft.AspNetCore.Mvc;
using TianLiUpdate.API.Data;
using TianLiUpdate.API.Models;
/*
namespace TianLiUpdate.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly ProjectContext _context;

        public FilesController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/<FilesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Files);
        }

        // GET api/<FilesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_context.Files.Find(id));
        }

        // POST api/<FilesController>
        [HttpPost]
        public IActionResult Post(File value)
        {
            _context.Files.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // PUT api/<FilesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, File value)
        {
            _context.Files.Update(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // DELETE api/<FilesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _context.Files.Remove(_context.Files.Find(id));
            _context.SaveChanges();
            return Ok();
        }
    }
}
*/