namespace HRSDmgmt.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        public string? Name { get; set; }

        public virtual List<Employee>? Employees { get; set; }

        public virtual List<Offer>? Offers { get; set; }
    }
}
