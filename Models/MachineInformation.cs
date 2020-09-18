using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class MachineInformation : BaseDatabaseField,IDatabaseObject
    {
        public Int64 LocationId { get; set; } = -1;
        public Int64 CustomerId { get; set; } = -1;

        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Baujahr")]
        public DateTime YearOfConstruction { get; set; }
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
        public ContactPartner ContactPerson { get; set; }

        [DisplayName("Beim Störungsfall kontaktieren")]
        public bool ContactByDefect { get; set; }

        [DisplayName("Zusätzliche Informationen"), DatabaseAttribute(Length = "200")]
        public string AdditionalInfo { get; set; }

        protected override string GetFullName()
        {
            return string.Format("{0}, {1}, {2}",Name, SerialNumber,YearOfConstruction);
        }

        public static string GetIndexFields()
        {
            return nameof(Name) + "," + nameof(SerialNumber) + "," + nameof(Description);
        }
    }
}

