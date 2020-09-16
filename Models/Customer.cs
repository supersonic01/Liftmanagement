using System;
using System.ComponentModel;

namespace Liftmanagement.Models
{
    public class Customer : Person
    {

        public int CustomerId { get; set; }

        [DisplayName("Firmenname")]
        public string CompanyName { get; set; }

        [DisplayName("Google Drive")]
        public string GoogleDriveFolderName { get; set; }
        public string GoogleDriveLink { get; set; }

        protected override string GetFullName()
        {
            return string.Format("{0}, {1} {2}", CompanyName, Postcode, City);
        }

        [DisplayName("Verwalter Firma")]
        public string AdministratorCompany  { get; set; }

        public static implicit operator string(Customer v)
        {
            throw new NotImplementedException();
        }
    }

   
}