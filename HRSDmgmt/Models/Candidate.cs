using Microsoft.Build.ObjectModelRemoting;
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSDmgmt.Models
{
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }   

        public int OfferId { get; set; }
        public virtual Offer? Offer { get; set; }

        public int EmployeeId { get; set; }    

        public virtual Employee? Employee { get; set; }
    }
}