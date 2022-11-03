using System.ComponentModel.DataAnnotations.Schema;

namespace HRSDmgmt.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client? Client { get; set; }  
    }
}
