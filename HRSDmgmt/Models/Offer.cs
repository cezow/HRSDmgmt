using System.ComponentModel.DataAnnotations.Schema;

namespace HRSDmgmt.Models
{
    public class Offer
    {
        public int OfferId { get; set; }
        public string? Name { get; set; }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client? Client { get; set; }

    }
}
