using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
  public class AdministratorCompany : BaseDatabaseField, IDatabaseObject
    {
        public Int64 CustomerId { get; set; }

        [DisplayName("Verwalter Firma"), DatabaseAttribute(DatabaseAttribute.NOT_NULL)]
        public string Name { get; set; }

        [DisplayName("Ansprechpartner")]       
        public List<ContactPartner> ContactPerson { get; set; } = new List<ContactPartner>();

        public static string GetIndexFields()
        {
            return nameof(Name);
        }

    }
}
