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

        [DisplayName("Ansprechpartner"), DatabaseAttribute(IsDbColumn = false, Updateable = false)]
        public List<ContactPartner> ContactPersons { get; set; } = new List<ContactPartner>();

        //private List<ContactPartner> contactPersons;

        //[DisplayName("Ansprechpartner"), DatabaseAttribute(Updateable = false)]
        //public List<ContactPartner> ContactPersons
        //{
        //    get { return contactPersons; }
        //    set { SetField(ref contactPersons, value); }
        //}


        [DatabaseAttribute(IsDbColumn = false, Updateable = false)]
        public GetFullNameDelegate GetParentFullName { get; set; }

        [ DatabaseAttribute(IsDbColumn =false, Updateable = false)]
        public List<ContactPartner> DeletedContactPersons { get; set; } = new List<ContactPartner>();

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

        public override string GetFullName()
        {
            return string.Format("{0}, {1}", Name, GetParentFullName());
        }

    }
}
