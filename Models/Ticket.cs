
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketingManagementSystemAPI.Models
{
    public class Ticket
    {
        public Ticket()
        {
          //  this.Department = Departmenttype.Admin;
          
            this.created_at = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        [Display(Name ="Issue")]
        public int IssuesId { get; set; }
        public Issues Issues { get; set; }  
        [Display(Name = "Subject")]
        public string subject { get; set; }
        [Display(Name = "Content")]
        public string content { get; set; }
        [Display(Name = "Priority")]
        public string ticketPriority { get; set; }
        [Display(Name = "Department")]

        public int DepartmentId { get; set; }


        [Display(Name = "DepartmentName")]

        public string DepartmentName { get; set; }

        [Display(Name ="Bay No")]
        public string BayNumber { get; set; }
     
        [Display(Name = "CreatedAt")]
        public DateTime created_at { get; set; }
        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }
        [Display(Name = "Assign")]
        public int? assignedPersonId { get; set; }
        public Signup assignedPerson { get; set; }
        [Display(Name = "Status")]
        public int TicketStatusId { get; set; } 
        public TicketStatus TicketStatus{ get; set; }
        [NotMapped]
        [Display(Name = "Attachment")]
        public IFormFile FileContent { get; set; }
        public string IssuesName { get; set; }
        public int StatusId { get; internal set; }

    }

    public class TicketTransaction
    {
        [Key]

        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedUTC { get; set; }
        public string AssignedBy { get; set; }
        public string AssignedTo { get; set; }
        public bool IsActive { get; set; }
        public string FilePath { get; set; }

    }

    public class TicketFormData
    {
        [Key]
        public int id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int IssuesId { get; set; }
        public string IssuesName { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }
        public DateTime created_at { get; set; }
        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }
        public string Subject { get; set; }
        public string BayNumber { get; set; }
        public string Content { get; set; }
        public string Priority { get; set; }
        public string AssignedBy { get; set; }
        public string AssignedTo { get; set; }
        public string FilePath { get; set; }


    }

    public class TicketStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
    //public class FileTransfer
    //{
    //    public IFormFile FileContent { get; set; }
    //    public string filepath { get; set; }
    //}

    public class TicketFormDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int IssuesId { get; set; }
        public string IssuesName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Subject { get; set; }
        public string BayNumber { get; set; }
        public string Content { get; set; }
        public string Priority { get; set; }
        public string FilePath { get; set; }
        public string AssignedBy { get; set; }
        public string AssignedTo { get; set; }
    }


}
