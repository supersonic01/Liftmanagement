using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class Person: BaseDatabaseField 
    {
        [DisplayName("Adresse"), DatabaseAttribute(Length = "50")]
        public string Address { get; set; }

        [DisplayName("PLZ"), DatabaseAttribute(Length = "10")]
        public string Postcode { get; set; }

        [DisplayName("Stadt"), DatabaseAttribute(Length = "50")]
        public string City { get; set; }
        
        [DisplayName("Merken")]
        public bool Selected { get; set; }

        [DisplayName("Zusätzliche Informationen"), DatabaseAttribute(Length = "300")]
        public string AdditionalInfo { get; set; }

        [DisplayName("Ansprechpartner"), DatabaseAttribute(Updateable = false)]
        public ContactPartner ContactPerson { get; set; } = new ContactPartner();

    }

}
