using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class Person
    {
        [DisplayName("Ansprechpartner")]
        public string ContactPerson { get; set; }
        [DisplayName("Adresse")]
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        [DisplayName("Tel. geschäftli.")]
        public string PhoneWork { get; set; }
        [DisplayName("Mobil")]
        public string Mobile { get; set; }
        [DisplayName("Merken")]
        public bool Selected { get; set; }
    }
}
