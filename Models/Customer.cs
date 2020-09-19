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

        protected override string GetFullName()
        {
            return string.Format("{0},{3} {1} {2}", CompanyName, Postcode, City, Address);
        }

        public static string GetIndexFields()
        {
            return nameof(CompanyName) + "," + nameof(Address) + "," + nameof(Postcode) + "," + nameof(City);
        }

    }

   
}