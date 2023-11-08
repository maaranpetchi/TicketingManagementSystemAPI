
namespace TicketingManagementSystemAPI.Models
{
    public class Issues
    {
        public int Id { get; set; }
        public string Issue { get; set; }
       
        public int DepartmentId { get; set; }
        public Department Departments { get; set; }

    }
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
    }
    public class Description
    {
        public Description()
        {
            this.description = "Your Ticket has been Processed!";
            this.IsDeleted = false;
        }
        public int Id { get; set; }
        public string description { get; set; }
        public bool IsDeleted { get; set; }
        public int issueID { get; set; }
        public Issues Issue { get; set; }
        
        
    }
}
