 using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace HRSDmgmt.Models
{
        public class Offer
        {
            [Key]
            [DisplayName("Id")]
            public int OfferId { get; set; }

            [Required(ErrorMessage = "Proszę podać nazwę firmy")]
            [DisplayName("Nazwa/Firma")]
            [MaxLength(30, ErrorMessage = "Nazwa oferty nie może być dłuższa niż 30 znaków")]
            public string? Name { get; set; }

            [Required(ErrorMessage = "Proszę podać opis oferty")]
            [Display(Name = "Opis zlecenia")]
            [MaxLength(255, ErrorMessage = "Opis oferty nie może być dłuższy niż 255 znaków")]
            public string? Description { get; set; }

            [Required]
            [DisplayName("Ilość wakatów")]
            public int Vacancy { get; set; }

            [Display(Name = "Publikacja")]
            [DataType(DataType.Date, ErrorMessage = "Niepoprawny format daty")]
            [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
            public System.DateTime AddDate { get; set; }

            [Required]
            [Display(Name = "Start prac")]
            [DataType(DataType.Date, ErrorMessage = "Niepoprawny format daty")]
            [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
            public System.DateTime StartDate { get; set; }

            [Display(Name = "Koniec prac")]
            [DataType(DataType.Date, ErrorMessage = "Niepoprawny format daty")]
            [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
            public System.DateTime EndDate { get; set; }

            [Required]
            [DisplayName("Czy oferta aktywna?")]
            [DefaultValue(false)]
            public bool Active { get; set; }

            [Required]
            [DisplayName("*Akceptuję")]
            [DefaultValue(true)]
            public bool Display { get; set; }

            public int? CompanyId { get; set; }
            [ForeignKey("CompanyId")]
            public virtual Company? Company { get; set; }

            public virtual ICollection<Candidate>? Candidates { get; set; }
        }
   }


