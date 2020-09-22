using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
  public class Recording: BaseDatabaseField
    {
        public DateTime Date { get; set; }

        [DisplayName("Datum")]
        public string DateVisual {

            get
            {
                return Date.ToString("dd.MM.yyyy");

            }
        }

        [DisplayName("Vorgangsnummer")]
        public long ProcessId {
            get
            {
                return Id;
            }
        }

        [DisplayName("Vorgang")]
        public string Process { get; set; }

        [DisplayName("Kostenart")]
        public string CostType { get; set; }

        [DisplayName("Anlage steht")]
        public bool MachineStoped { get; set; }

        [DisplayName("Zeitkritisch")]
        public DateTime Timesensitive { get; set; }

        [DisplayName("Rechnungsnummer")]
        public string InvoiceNumber { get; set; }

        [DisplayName("Prüfmmangel Stufe\n(TÜV)")]
        public int IssueLevel { get; set; }

        [DisplayName("Meldung von")]
        public string ReportedFrom { get; set; }

        [DisplayName("Grund")]
        public string Reason { get; set; }

        [DisplayName("Ablage")]
        public bool Storage { get; set; }

        [DisplayName("Nächste Aktion")]
        public string NextStep { get; set; }

        [DisplayName("Bearbeiter")]
        public string PersonResponsible { get; set; }

        [DisplayName("Freigabe")]
        public string ReleaseFrom { get; set; }

        [DisplayName("Auftraggeber\ninformiert")]
        public bool CustomerInformed { get; set; }

        [DisplayName("Auftraggeber\nmöchte")]
        public string CustomerPrefers { get; set; }

        [DisplayName("Angebotspreis")]
        public double OfferPrice  { get; set; }

        [DisplayName("Rechnungshöhe\nkorrekt")]
        public double BillingAmountCorrect { get; set; }

        [DisplayName("Ausführung\nkorrekt")]
        public bool ExecutionCorrect { get; set; }

        [DisplayName("Vor Ort geprüft")]
        public string VerifiedOnSpot { get; set; }
    }
}
