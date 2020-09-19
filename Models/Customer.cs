using Liftmanagement.Helper;
using System;
using System.ComponentModel;

namespace Liftmanagement.Models
{
    public class Customer : Person, IDatabaseObject
    {
        [DisplayName("Kundenname"), DatabaseAttribute(Length ="100")]
        public string CompanyName { get; set; }

        [DisplayName("Google Drive")]
        public string GoogleDriveFolderName { get; set; }

        [DisplayName("Google Drive Ordner"),DatabaseAttribute(Length = "200")]
        public string GoogleDriveLink { get; set; }


        [DisplayName("Verwalter Firma"), DatabaseAttribute(Updateable = false)]
        public AdministratorCompany Administrator { get; set; } = new AdministratorCompany();

        public string FullNameAdministrator
        {
            get
            {
                return GetFullNameAdministrator();
            }
        }

        public static implicit operator string(Customer v)
        {
            throw new NotImplementedException();
        }

        protected override string GetFullName()
        {
            return string.Format("{0},{3} {1} {2}", CompanyName, Postcode, City, Address);
        }


        protected  string GetFullNameAdministrator()
        {
            return string.Format("{0}, {1}", Administrator.Name, GetFullName());
        }

        public static string GetIndexFields()
        {
            return nameof(CompanyName) + "," + nameof(Address) + "," + nameof(Postcode) + "," + nameof(City);
        }
    }

   
}