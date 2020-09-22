using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Helper;

namespace Liftmanagement.Models
{
   public class ManagementOverviewFilter: DisplayNameRetriever
    {
        public long LocationId { get; set; }
        public long CustomerId { get; set; }
        public long MachineInformationId { get; set; }

        [DisplayName("Verwalter Firma")]
        public string Administrator { get; set; }

        [DisplayName("Kundenname")]
        public string CompanyName { get; set; }

        [DisplayName("Standort")]
        public string Location { get; set; }

        [DisplayName("Hersteller")]
        public string Manufacturer { get; set; }

        [DisplayName("Fabriknummer")]
        public string SerialNumber { get; set; }

        [DisplayName("Baujahr")]
        public string YearOfConstruction { get; set; }
      


        protected override string GetFullName()
        {
            return (Administrator + CompanyName + Location + Manufacturer + SerialNumber + YearOfConstruction).Trim().ToLower();
        }
    }
}
