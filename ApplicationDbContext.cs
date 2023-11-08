using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Sockets;
using TicketingManagementSystemAPI.Models;

namespace TicketingManagementSystemAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Signup> Signup{ get; set; }
        public DbSet<Ticket> Ticket { get; set; }

        //public DbSet<ticket.Ticket_status> Tickets { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Issues> Issues { get; set; }
        public DbSet<Description> Description { get; set; }
        public DbSet<TicketTransaction> TicketTransaction { get; set; }
        public DbSet<TicketStatus> TicketStatus { get; set; }
        
      public DbSet<TicketFormData> TicketFormData { get; set; }


    }
}
