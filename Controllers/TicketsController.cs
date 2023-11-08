using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Sockets;
using TicketingManagementSystemAPI.Interfaces;
using TicketingManagementSystemAPI.Models;

namespace TicketingManagementSystemAPI.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SmtpOptions smtpOptions { get; }
        private readonly ITicketHandler _ticketHandler;
        public TicketsController(ApplicationDbContext context, IOptions<SmtpOptions> options, ITicketHandler ticketHandler)
        {
            smtpOptions = options.Value;
            _context = context;
            _ticketHandler = ticketHandler;
        }


        ///Dropdown values//
           // GET: api/Department
        [HttpGet("department")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Department.ToListAsync();
        }

        // GET: api/Department/5
        [HttpGet("department/{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }




        // GET: api/tickets
        [HttpGet("tickets")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Ticket.ToListAsync();
        }


        //**************Status***********************//
        [HttpGet("status")]
        public async Task<ActionResult<IEnumerable<TicketStatus>>> GetStatus()
        {
            return await _context.TicketStatus.ToListAsync();
        }


        // GET: api/tickets/5
        [HttpGet("Tickets/{id}")]
        public async Task<ActionResult<Ticket>> GetTickets(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        //////Issues////////////////////


        [HttpGet("issues")]
        public async Task<ActionResult<IEnumerable<Issues>>> GetIssues()
        {
            return await _context.Issues.ToListAsync();
        }

        [HttpGet("{departmentId}")]
        public async Task<ActionResult<IEnumerable<Issues>>> GetIssuesByDepartment(int departmentId)
        {
            var issues = await _context.Issues
                .Where(i => i.DepartmentId == departmentId)
                .ToListAsync();

            if (issues == null || issues.Count == 0)
            {
                return NotFound("No issues found for the specified department.");
            }

            return issues;
        }

        [HttpPost("createTicket")]
        public async Task<IActionResult> PostTicket([FromBody] TicketFormDto ticketFormDto)
        {
            // Create TicketFormData object
            var ticketFormData = new TicketFormData
            {
                DepartmentId = ticketFormDto.DepartmentId,
                DepartmentName = ticketFormDto.DepartmentName,
                IssuesId = ticketFormDto.IssuesId,
                IssuesName = ticketFormDto.IssuesName,
                StatusId = ticketFormDto.StatusId,
                StatusName = ticketFormDto.StatusName,
                created_at = ticketFormDto.CreatedAt,
                CreatedBy = ticketFormDto.CreatedBy,
                Subject = ticketFormDto.Subject,
                BayNumber = ticketFormDto.BayNumber,
                Content = ticketFormDto.Content,
                Priority = ticketFormDto.Priority,
                FilePath = ticketFormDto.FilePath,

            };

            _context.TicketFormData.Add(ticketFormData);
            await _context.SaveChangesAsync();

            // Create TicketTransaction object
            var ticketTransaction = new TicketTransaction
            {
                TicketId = ticketFormData.id,
                Status = "A new ticket created successfully",
                CreatedUTC = DateTime.UtcNow,
                AssignedBy = string.Empty, // Assign appropriate value based on your logic
                AssignedTo = string.Empty,
                FilePath = ticketFormDto.FilePath,
// Assign appropriate value based on your logic
                IsActive = true
            };

            _context.TicketTransaction.Add(ticketTransaction);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Ticket created successfully",ticketFormData });
        }
        [HttpGet("getissues")]
        public async Task<ActionResult<IEnumerable<TicketFormData>>> GetTicketIssues(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                // Handle invalid input, return bad request response
                return BadRequest("Invalid first name");
            }

            var loggedInUser = await _context.Signup.SingleOrDefaultAsync(u => u.Firstname == firstName);
            if (loggedInUser == null)
            {
                // Handle invalid user, return not found response
                return NotFound("User not found");
            }

            IQueryable<TicketFormData> query = _context.TicketFormData;
            if (!loggedInUser.isSuperAdmin)
            {
                // If user is not super admin, filter tickets based on CreatedBy field
                query = query.Where(t => t.CreatedBy == firstName);
            }

            var ticketIssues = await query.ToListAsync();
            return ticketIssues;
        }



    }


}