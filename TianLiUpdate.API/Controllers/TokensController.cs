using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TianLiUpdate.API.Data;
using TianLiUpdate.API.Models;

namespace TianLiUpdate.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ProjectContext _context;
        public TokensController(ProjectContext context)
        {
            _context = context;
        }
        
        /*
        // GET: /Tokens
        [HttpGet]
        public ActionResult<IEnumerable<Token>> GetTokens()
        {
            if (_context.Tokens == null)
            {
                return NotFound("Entity set 'Tokens'  is null.");
            }
            return _context.Tokens.ToList();
        }
        // GET: api/Tokens/5
        [HttpGet("{id}")]
        public ActionResult<Token> GetToken(Guid? id)
        {
            if (_context.Tokens == null)
            {
                return NotFound("Entity set 'Tokens'  is null.");
            }
            var token = _context.Tokens.Find(id);
            if (token == null)
            {
                return NotFound("Token not found.");
            }
            return token;
        }
        // PUT: api/Tokens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutToken(Guid? id, Token token)
        {
            if (id != token.Id)
            {
                return BadRequest("Token id is not equal to the id in the request body.");
            }
            _context.Entry(token).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TokenExists(id))
                {
                    return NotFound("Token not found.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        
        // POST: api/Tokens
        // Create a new token
        [HttpPost("Create")]
        public ActionResult<Token> PostToken()
        {
            var token = new Token{
                Id = Guid.NewGuid(),
                TokenString = Guid.NewGuid().ToString(),
                LastUseTime = DateTime.Now
            };
            _context.Tokens.Add(token);
            _context.SaveChanges();
            return Ok(token.TokenString);
        }
        */
        [HttpPost("CreateTokenString")]
        public ActionResult<Token> PostTokenString(string tokenString, string suToken)
        {
            if (_context.Tokens.Where(t => t.TokenString == suToken).Count() == 0)
            {
                return Problem("Super user token is not valid.");
            }
            if (_context.Tokens == null)
            {
                return Problem("Entity set 'ArtifactContext.Tokens'  is null.");
            }
            var token = new Token
            {
                TokenID = Guid.NewGuid(),
                TokenString = tokenString,
                LastUseTime = DateTime.Now
            };
            _context.Tokens.Add(token);
            _context.SaveChanges();
            return Ok(token); //CreatedAtAction("GetToken", new { id = token.Id }, token);
        }
        /*
        // DELETE: api/Tokens/5
        [HttpDelete("{id}")]
        public IActionResult DeleteToken(Guid? id)
        {
            if (_context.Tokens == null)
            {
                return NotFound("Entity set 'Tokens'  is null.");
            }
            var token = _context.Tokens.Find(id);
            if (token == null)
            {
                return NotFound("Token not found.");
            }
            _context.Tokens.Remove(token);
            _context.SaveChanges();
            return NoContent();
        }
        */
        private bool TokenExists(Guid? id)
        {
            return (_context.Tokens?.Any(e => e.TokenID == id)).GetValueOrDefault();
        }
    }
}