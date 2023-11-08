using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketingManagementSystemAPI.Models;

namespace TicketingManagementSystemAPI.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var user = _context.Signup.FirstOrDefault(x => x.EmployeeId == model.EmployeeId && x.Password == model.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            // Return the Firstname property of the user when login is successful
            return Ok(new { message = "Login successful", User = user });
        }


        [HttpGet]
        [Route("logout")]//hited
        public bool ExternalLogin()
        {
            // FormsAuthentication.SignOut();
            HttpContext.SignOutAsync();
            return true;
        }
    }
}
