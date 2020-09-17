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

        [DatabaseAttribute(Length = "200")]
        public string GoogleDriveLink { get; set; }


        [DisplayName("Verwalter Firma")]
        public AdministratorCompany Administrator { get; set; } = new AdministratorCompany();

        public static implicit operator string(Customer v)
        {
            throw new NotImplementedException();
        }

        protected override string GetFullName()
        {
            return string.Format("{0}, {1} {2}", CompanyName, Postcode, City);
        }

        public static string GetIndexFields()
        {
            return nameof(CompanyName) + "," + nameof(Address) + "," + nameof(Postcode) + "," + nameof(City);
        }
    }

   
}