using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
   public class RecordAgreementType
    {
        public Helper.Helper.TTypeMangement AgreementType { get; set; }
        public MaintenanceAgreement MaintenanceAgreement { get; set; }

        public override string ToString()
        {
            return string.Format("Wartungsvertrag {0}", MaintenanceAgreement.AgreementDate.Date.ToString("dd.MM.yyyy")) ;
        }

    }


   
}
