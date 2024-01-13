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
        
        // POST: api/Tokens
        // Create a new token
        [HttpGet("Create")]
        public ActionResult<Token> PostToken(string suToken)
        {
            var su = GetSuperToken();
            if(su==null)
            {
                return NotFound("Super token not found");
            }
            if (suToken != su.TokenString)
            {
                return Unauthorized();
            }
            var token = new Token{
                TokenID = Guid.NewGuid(),
                TokenString = Guid.NewGuid().ToString(),
                LastUseTime = DateTime.Now
            };
            _context.Tokens.Add(token);
            _context.SaveChanges();
            return Ok(token.TokenString);
        }

        private Token? GetSuperToken()
        {
            return _context.Tokens.Find(Guid.Empty.ToString());
        }
    }
}