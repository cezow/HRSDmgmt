using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace HRSDmgmt.Models
{
    public class Employee
    {
        [Key]
        [DisplayName("Identyfikator pracownika")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Wprowadź imię pracownika")]
        [Display(Name = "Imię pracownika:")]
        [MaxLength(20)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Wprowadź nazwisko pracownika")]
        [Display(Name = "Nazwisko pracownika:")]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [NotMapped]
        [Display(Name = "Pan/Pani:")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [Display(Name = "Numer telefonu")]
        [MaxLength(20, ErrorMessage = "Numer telefonu nie może być dłuższe niż 15 znaków")]
        public string? Mobile { get; set; }

        [Display(Name = "Adres e-mail")]
        [MaxLength(30, ErrorMessage = "Adres e-mail nie może być dłuższy niż 15 znaków")]
        public string? Email { get; set; }

        [Display(Name = "Wykształcenie pracownika:")]
        [MaxLength(255, ErrorMessage = "Zbyt długi opis - skróć do 255 znaków")]
        public string? Education { get; set; }

        [Display(Name = "Zawód pracownika:")]
        [MaxLength(15, ErrorMessage = "Zbyt długi opis - skróć do 15 znaków")]
        public string? Profession { get; set; }

        [Display(Name = "Umiejętności pracownika:")]
        [MaxLength(255, ErrorMessage = "Zbyt długi opis - skróć do 255 znaków")]
        public string? Skills { get; set; }

        [Display(Name = "Doświadczenie pracownika:")]
        [MaxLength(255, ErrorMessage = "Zbyt długi opis - skróć do 255 znaków")]
        public string? Experience { get; set; }

        [DisplayName("CV pracownika:")]
        [FileExtensions(Extensions = " .pdf, .doc, .docx", ErrorMessage = "Niepoprawne rozszerzenie pliku.")]
        [MaxLength(128)]
        public string? CV { get; set; }

        [DisplayName("Użytkownik pracownika")]
        public string? Id { get; set; }

        [ForeignKey("Id")]
        public virtual AppUser? User { get; set; }


    }
}
