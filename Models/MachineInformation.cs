using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Liftmanagement.Models
{
    public class MachineInformation : GoogleDrive ,IDatabaseObject
    {
        public long LocationId { get; set; } = -1;
        public long CustomerId { get; set; } = -1;

        [DisplayName("Hersteller")]
        public string Name { get; set; }

       
        private DateTime yearOfConstruction = Helper.Helper.DefaultDate;
        [DisplayName("Baujahr")]
        public DateTime YearOfConstruction
        {
            get { return yearOfConstruction; }
            set
            {
                if (value.Date < Helper.Helper.DefaultDate.Date)
                {
                    value = Helper.Helper.DefaultDate.Date;
                }
                yearOfConstruction = value;
            }
        }
        
        [DisplayName("Fabriknummer")]
        public string SerialNumber { get; set; }
        [DisplayName("Haltestellen")]
        public int HoldingPositions { get; set; }
        [DisplayName("Zugänge")]
        public int Entrances { get; set; }
        [DisplayName("Zuladung in Kg")]
        public int Payload { get; set; }

        [DisplayName("Beschreibung"), DatabaseAttribute(Length = "200")]
        public string Description { get; set; }

        [DisplayName("Ansprechpartner"), DatabaseAttribute(Updateable = false)]
        public ContactPartner ContactPerson { get; set; } = new ContactPartner();

        public override string GetFullName()
        {
            return string.Format("{0}, {1}, {2}",Name, SerialNumber,YearOfConstruction.ToString("dd.MM.yyyy"));
        }

        public static string GetIndexFields()
        {
            return nameof(Name) + "," + nameof(SerialNumber) + "," + nameof(Description);
        }

        public override string ToString()
        {
            return GetFullName();
        }
    }
}

