using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Net.Sockets;

namespace TicketingManagementSystemAPI.Models
{
    public class Signup
    {
        public Signup()
        {
            //this.Department = Departmenttype.Admin;
            this.isActive = true;
        }
        [Key]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [DisplayName("Department")]
        public int DepartmentsId { get; set; }
        public Department Departments { get; set; }
        public string Email { get; set; }
        public string phoneno { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [DisplayName("Password")]
        [RegularExpression(@"^[a-zA-Z0-9\s]{8,15}$", ErrorMessage = "Please enter more than 8 character and special characters are not allowed")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool isSuperAdmin { get; set; }
        public bool isActive { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
    public class SignUpDto
    {

        public SignUpDto()
        {
            //this.Department = Departmenttype.Admin;
            this.isActive = true;
        }
        [Key]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [DisplayName("Department")]
        public int DepartmentsId { get; set; }
        public Department Departments { get; set; }
        public string Email { get; set; }
        public string phoneno { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [DisplayName("Password")]
        [RegularExpression(@"^[a-zA-Z0-9\s]{8,15}$", ErrorMessage = "Please enter more than 8 character and special characters are not allowed")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Does Not Match!")]
        [Display(Name = "Confirm Password")]
        public string Confirm_Password { get; set; }
        public bool isSuperAdmin { get; set; }
        public bool isActive { get; set; }

    }
}
