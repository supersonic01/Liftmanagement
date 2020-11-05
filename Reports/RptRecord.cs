using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Reports
{
   public class RptRecord
    {
        [DisplayName("Vorgang")]
        public string Process { get; set; }

        [DisplayName("Kostenart")]
        public string CostType { get; set; }

        [DisplayName("Rechnungsnummer")]
        public string InvoiceNumber { get; set; }

        [DisplayName("Angebotspreis")]
        public double OfferPrice { get; set; }

        [DisplayName("Tatsächlicher Rechnungsbetrag")]
        public double BillingAmountCorrect { get; set; }

    }
}
