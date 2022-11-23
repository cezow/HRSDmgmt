using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSDmgmt.Models
{
    public class Company
    {
        [Key]
        [DisplayName("Identifikator firmy")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę firmy")]
        [Display(Name = "Nazwa firmy:")]
        [MaxLength(50, ErrorMessage = "Nazwa firmy nie może być dłuższa niż 50 znaków")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Proszę podać nr NIP firmy")]
        [Display(Name = "NIP firmy:")]
        [MaxLength(10, ErrorMessage = "NIP firmy nie może być dłuższa niż 10 znaków")]
        [MinLength(10, ErrorMessage = "NIP firmy nie może być krótsza niż 10 znaków")]
        public long NIP { get; set; }

        [Required(ErrorMessage = "Proszę podać opis firmy")]
        [Display(Name = "Opis firmy:")]
        [MaxLength(255, ErrorMessage = "Opis firmy nie może być dłuższy niż 255 znaków")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Proszę podać adres firmy")]
        [Display(Name = "Adres firmy:")]
        [MaxLength(50, ErrorMessage = "Adres firmy nie może być dłuższy niż 50 znaków")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Proszę podać kraj firmy")]
        [Display(Name = "Kraj firmy:")]
        [MaxLength(15, ErrorMessage = "Nazwa kategorii nie może być dłuższa niż 15 znaków")]
        public string? Country { get; set; }

        [Display(Name = "Osoba do kontaktu:")]
        [MaxLength(30, ErrorMessage = "Imię i nazwisko osoby do kontaktu nie może być dłuższe niż 30 znaków")]
        public string? ContactPerson { get; set; }

        [Display(Name = "Numer telefonu")]
        [MaxLength(15, ErrorMessage = "Numer telefonu nie może być dłuższe niż 15 znaków")]
        public string? Mobile { get; set; }

        [Display(Name = "Adres e-mail")]
        [MaxLength(30, ErrorMessage = "Adres e-mail nie może być dłuższy niż 15 znaków")]
        public string? Email { get; set; }

        [Display(Name = "Logo firmy")]
        [MaxLength(128)]
        [FileExtensions(Extensions = ".jpg, .png, .gif", ErrorMessage = "Niepoprawne rozszerzenie pliku.")]
        public string? Logo { get; set; }

        [Required]
        [DisplayName("Czy firma aktywna?")]
        [DefaultValue(false)]
        public bool Active { get; set; }

        [Required]
        [DisplayName("Czy wyświetlać?")]
        [DefaultValue(true)]
        public bool Display { get; set; }

        public virtual List<Employee>? Employees { get; set; }

        public virtual List<Offer>? Offers { get; set; }

        [DisplayName("Użytkownik firmy")]
        public string? Id { get; set; }

        [ForeignKey("Id")]
        public virtual AppUser? User { get; set; }
    }
}
