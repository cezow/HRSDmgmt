using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace HRSDmgmt.Models
{
    public class Offer
    {
        [Key]
        [DisplayName("Identyfikator oferty")]
        public int OfferId { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę oferty")]
        [DisplayName("Identyfikator oferty")]
        [MaxLength(30, ErrorMessage = "Nazwaoferty nie może być dłuższa niż 30 znaków")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Proszę podać opis oferty")]
        [Display(Name = "Opis zlecenia:")]
        [MaxLength(255, ErrorMessage = "Opis oferty nie może być dłuższy niż 255 znaków")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Data początku prac")]
        [DataType(DataType.Date, ErrorMessage = "Niepoprawny format daty")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public System.DateTime StartDate { get; set; }

        [Display(Name = "Data zakończenia prac")]
        [DataType(DataType.Date, ErrorMessage = "Niepoprawny format daty")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public System.DateTime EndDate { get; set; }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Company? Client { get; set; }

        public virtual List<Employee>? Employees { get; set; } 
    }
}
