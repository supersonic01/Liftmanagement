using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Liftmanagement.Models
{
    public class Person: BaseDatabaseField
    {
        //[DisplayName("Adresse"), DatabaseAttribute(Length = "50")]
        //public string Address { get; set; }

        private string address;

        [DisplayName("Adresse"), DatabaseAttribute(Length = "50")]
        public string Address
        {
            get { return address; }
            set { SetField(ref address, value); }
        }

        private string postcode;

        [DisplayName("PLZ"), DatabaseAttribute(Length = "10")]
        public string Postcode
        {
            get { return postcode; }
            set { SetField(ref postcode, value); }
        }

        private string city;

        [DisplayName("Stadt"), DatabaseAttribute(Length = "50")]
        public string City
        {
            get { return city; }
            set { SetField(ref city, value); }
        }
        
        // [DisplayName("Merken")]
        public bool Selected { get; set; }

        [DisplayName("Zusätzliche Informationen"), Database(Length = "300"),Display(DataGridColumnWidth = DataGridLengthUnitType.Star)]
        public string AdditionalInfo { get; set; }

      [DatabaseAttribute(Updateable = false)]
        public ContactPartner ContactPerson { get; set; } = new ContactPartner();



        private string googleDriveFolderName;

        [DisplayName("Google Drive")]
        public string GoogleDriveFolderName
        {
            get { return googleDriveFolderName; }
            set { SetField(ref googleDriveFolderName, value); }
        }
        
        private string googleDriveLink;

        [DatabaseAttribute(Length = "200")]
        public string GoogleDriveLink
        {
            get { return googleDriveLink; }
            set { SetField(ref googleDriveLink, value); }
        }

        public string GetPostcodeCity()
        {
            return Postcode + ", " + City;
        }
    }

}
