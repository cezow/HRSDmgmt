using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace HRSDmgmt.Models
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "Imię")]
        [MaxLength(20)]
        public string? FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [MaxLength(50)]
        public string? LastName { get; set; }


        [NotMapped]
        [Display(Name = "Pan/Pani")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        
        [Display(Name = "Konto firmowe?")]
        [DefaultValue(true)]
        public bool IsCompany { get; set; }

        public virtual Company? Company { get; set; }
        public virtual Employee? Employee { get; set; }


    }
}
