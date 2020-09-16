using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
  public class AdministratorCompany : DisplayNameRetriever
    {
        [DisplayName("Verwalter Firma")]
        public string Name { get; set; }

        [DisplayName("Ansprechpartner")]
        public List<ContactPartner> ContactPerson { get; set; }
    }
}
