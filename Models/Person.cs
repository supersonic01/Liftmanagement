using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class Person: DisplayNameRetriever
    {

        [DisplayName("Adresse")]
        public string Address { get; set; }
        [DisplayName("PLZ")]
        public string Postcode { get; set; }
        [DisplayName("Stadt")]
        public string City { get; set; }
        
        [DisplayName("Merken")]
        public bool Selected { get; set; }

        [DisplayName("Zusätzliche Informationen")]
        public string AdditionalInfo { get; set; }

        [DisplayName("Ansprechpartner")]
        public ContactPartner ContactPerson { get; set; }

    }

}
