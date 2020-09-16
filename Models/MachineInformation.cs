using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class MachineInformation: DisplayNameRetriever
    {
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public int MachineInformationId { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Baujahr")]
        public string YearOfConstruction { get; set; }
        [DisplayName("Fabriknummer")]
        public string SerialNumber { get; set; }
        [DisplayName("Haltestellen")]
        public int HoldingPositions { get; set; }
        [DisplayName("Zugänge")]
        public int Entrances { get; set; }
        [DisplayName("Zuladung in Kg")]
        public int Payload { get; set; }

        [DisplayName("Beschreibung")]
        public string Description { get; set; }

        [DisplayName("Ansprechpartner")]
        public string ContactPerson { get; set; }

        [DisplayName("Tel. geschäftli.")]
        public string PhoneWork { get; set; }
        [DisplayName("Mobil")]
        public string Mobile { get; set; }

        [DisplayName("Beim Störungsfall kontaktieren")]
        public bool ContactByDefect { get; set; }

        [DisplayName("Zusätzliche Informationen")]
        public string AdditionalInfo { get; set; }

        protected override string GetFullName()
        {
            return string.Format("{0}, {1}, {2}",Name, SerialNumber,YearOfConstruction);
        }
    }
}

