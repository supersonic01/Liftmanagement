using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Helper;

namespace Liftmanagement.Models
{
    public class Record : GoogleDrive, IDatabaseObject
    {

        public long LocationId { get; set; }
        public long CustomerId { get; set; }
        public long MachineInformationId { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [DisplayName("Datum"), DatabaseAttribute(IsDbColumn = false, Updateable = false)]
        public string DateVisual
        {

            get
            {
                return Date.ToString("dd.MM.yyyy");

            }
        }

        [DisplayName("Vorgangsnummer")]
        public string ProcessNo { get; private set; }


        //[DisplayName("Vorgangsnummer")]
        //public long ProcessId {
        //    get
        //    {
        //        return Id;
        //    }
        //}

        [DisplayName("Vorgang")]
        public string Process { get; set; }

        [DisplayName("Kostenart")]
        public string CostType { get; set; }

        [DisplayName("Anlage steht")]
        public bool MachineStoped { get; set; }

        [DisplayName("Zeitkritisch")]
        public DateTime? Timesensitive { get; set; }

        [DisplayName("Rechnungsnummer")]
        public string InvoiceNumber { get; set; }

        [DisplayName("Prüfmmangel Stufe\n(TÜV)")]
        public int IssueLevel { get; set; }

        [DisplayName("Meldung von")]
        public string ReportedFrom { get; set; }

        [DisplayName("Grund"), DatabaseAttribute(Length = "200")]
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
        public double OfferPrice { get; set; }

        [DisplayName("Rechnungshöhe\nkorrekt")]
        public double BillingAmountCorrect { get; set; }

        [DisplayName("Ausführung\nkorrekt")]
        public bool ExecutionCorrect { get; set; }

        [DisplayName("Vor Ort geprüft")]
        public string VerifiedOnSpot { get; set; }

        private bool processCompleted;

        [DisplayName("Vorgang abgeschlossen")]
        public bool ProcessCompleted
        {
            get { return processCompleted; }
            set { SetField(ref processCompleted, value); }
        }


        [DisplayName("Vorgang abgeschlossen durch")]
        public string ProcessCompletedPerson { get; set; }

        [DisplayName("Vertragsart")]
        public Helper.Helper.TTypeMangement AgreementType { get; private set; }

        public long AgreementId { get; private set; }

        private RecordAgreementType agreement;
        [DatabaseAttribute(IsDbColumn = false, Updateable = false)]
        public RecordAgreementType Agreement
        {
            get { return agreement; }
            set
            {
                agreement = value;
                Task.Factory.StartNew(() => SetAgreement());
            }
        }

        private void SetAgreement()
        {
            if (agreement == null)
                return;

            AgreementType = agreement.AgreementType;
            switch (agreement.AgreementType)
            {
                case Helper.Helper.TTypeMangement.MaintenanceAgreement:
                    AgreementId = agreement.MaintenanceAgreement.Id;
                    break;
                case Helper.Helper.TTypeMangement.EmergencyAgreement:
                    AgreementId = agreement.EmergencyAgreement.Id;
                    break;
            }
        }

        public void SetAgreementType(int agreementType)
        {
            AgreementType = (Helper.Helper.TTypeMangement)agreementType;
        }

        public void SetAgreementId(long agreementId)
        {
            AgreementId = agreementId;
        }

        public override string GetFullName()
        {
            return string.Format("{0}, {1}, {2}", Process, CostType, InvoiceNumber);
        }

        public static string GetIndexFields()
        {
            return nameof(ProcessNo) + "," + nameof(Process) + "," + nameof(CostType) + "," + nameof(InvoiceNumber) + "," + nameof(Reason);
        }

        public void SetProcessNo(string processNo)
        {
            this.ProcessNo = processNo;
        }
    }
}
