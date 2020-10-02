using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Liftmanagement.Models
{
    public class Person: BaseDatabaseField 
    {
        [DisplayName("Adresse"), DatabaseAttribute(Length = "50")]
        public string Address { get; set; }

        [DisplayName("PLZ"), DatabaseAttribute(Length = "10")]
        public string Postcode { get; set; }

        [DisplayName("Stadt"), DatabaseAttribute(Length = "50")]
        public string City { get; set; }
        
       // [DisplayName("Merken")]
        public bool Selected { get; set; }

        [DisplayName("Zusätzliche Informationen"), Database(Length = "300"),Display(DataGridColumnWidth = DataGridLengthUnitType.Star)]
        public string AdditionalInfo { get; set; }

        //[DisplayName("Ansprechpartner"), DatabaseAttribute(Updateable = false)]
        [DatabaseAttribute(Updateable = false)]
        public ContactPartner ContactPerson { get; set; } = new ContactPartner();

        [DisplayName("Google Drive")]
        public string GoogleDriveFolderName { get; set; }

        // [DisplayName("Google Drive Ordner"), DatabaseAttribute(Length = "200")]
        [DatabaseAttribute(Length = "200")]
        public string GoogleDriveLink { get; set; }

        public string GetPostcodeCity()
        {
            return Postcode + ", " + City;
        }
    }

}
