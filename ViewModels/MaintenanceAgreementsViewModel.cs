using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.ViewModels
{
   public class MaintenanceAgreementsViewModel : ViewModel
    {

        private List<MaintenanceAgreement> machineInformations = new List<MaintenanceAgreement>();

        public List<MaintenanceAgreement> MaintenanceAgreements
        {
            get { return machineInformations; }
            set { machineInformations = value; }
        }


        public MaintenanceAgreementsViewModel()
        {
            machineInformations = TestData.GetMaintenanceAgreements();
        }
    }
}
