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
        public long CustomerId { get; set; }

        [DisplayName("Verwalter Firma"), DatabaseAttribute(DatabaseAttribute.NOT_NULL)]
        public string Name { get; set; }

        [DisplayName("Ansprechpartner"), DatabaseAttribute(Updateable = false)]
        public List<ContactPartner> ContactPersons { get; set; } = new List<ContactPartner>();

        [ DatabaseAttribute(Updateable = false)]
        public GetFullNameDelegate GetParentFullName { get; set; }

        public AdministratorCompany()
        {
           
        }

        public AdministratorCompany(BaseDatabaseField baseField)
        {
            CreatedPersonName = baseField.CreatedPersonName;
            ModifiedPersonName = baseField.ModifiedPersonName;
            ReadOnly = baseField.ReadOnly;
            UsedBy = baseField.UsedBy;
            CustomerId = baseField.Id;
        }

        public static string GetIndexFields()
        {
            return nameof(Name);
        }

        public override string ToString()
        {
            return Name;
        }

        protected override string GetFullName()
        {
            return string.Format("{0}, {1}", Name, GetParentFullName());
        }

    }
}
