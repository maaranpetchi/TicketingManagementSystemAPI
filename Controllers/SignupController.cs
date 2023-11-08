using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketingManagementSystemAPI.Models;

namespace TicketingManagementSystemAPI.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SignupController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Signup signup)
        {
            _context.Signup.Add(signup);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registration successful" });
        }
    }
}
