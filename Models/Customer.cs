using Liftmanagement.Helper;
using System;
using System.ComponentModel;
using Attribute = Org.BouncyCastle.Asn1.Cms.Attribute;

namespace Liftmanagement.Models
{
    public class Customer : Person, IDatabaseObject
    {
        [DisplayName("Kundenname"), DatabaseAttribute(Length ="100", Attribute = DatabaseAttribute.NOT_NULL)]
        public string CompanyName { get; set; }

        private AdministratorCompany administrator = new AdministratorCompany();

        [DisplayName("Verwalter Firma"), DatabaseAttribute(Updateable = false)]
        public AdministratorCompany Administrator
        {
            get
            {
                administrator.GetParentFullName = new GetFullNameDelegate(GetFullName);
                return administrator;
            }
            set { administrator = value; }
        }
        
        public Customer()
        {

        }
        public override string ToString()
        {
            return CompanyName;
        }

        public override string GetFullName()
        {
            return string.Format("{0}, {3}, {1} {2}", CompanyName, Postcode, City, Address);
        }

        public static string GetIndexFields()
        {
            return nameof(CompanyName) + "," + nameof(Address) + "," + nameof(Postcode) + "," + nameof(City);
        }

        public static Customer GetDummy(string name = "** Alle **")
        {
            return new Customer { CompanyName = name };
        }

    }

   
}