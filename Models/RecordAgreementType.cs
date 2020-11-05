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
        public EmergencyAgreement EmergencyAgreement { get; set; }

        public override string ToString()
        {
            string value= "";
            switch (AgreementType)
            {
                case Helper.Helper.TTypeMangement.EmergencyAgreement:
                    value= string.Format("Notrufvertrag {0}", EmergencyAgreement.AgreementDate.Date.ToString("dd.MM.yyyy"));
                    break;
                case Helper.Helper.TTypeMangement.MaintenanceAgreement:
                    value = string.Format("Wartungsvertrag {0}", MaintenanceAgreement.AgreementDate.Date.ToString("dd.MM.yyyy"));

                    break;
            }

            return value;
        }

    }


   
}
