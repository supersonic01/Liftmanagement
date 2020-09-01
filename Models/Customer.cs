using System.ComponentModel;

namespace Liftmanagement.Models
{
    public class Customer : Person
    {

        public int CustomerId { get; set; }

        [DisplayName("Firmenname")]
        public string CompanyName { get; set; }
    }
}