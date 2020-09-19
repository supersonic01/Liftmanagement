using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.ViewModels
{
   public class ManagementViewModel : MasterDataInfoViewModel
    {
        public List<string> NotVisibleColumns { get; set; } = new List<string>();

        private List<MaintenanceAgreement> maintenanceAgreements = new List<MaintenanceAgreement>();

        public List<MaintenanceAgreement> MaintenanceAgreements
        {
            get { return maintenanceAgreements; }
            set { SetField(ref maintenanceAgreements, value); }
        }

        private ObservableCollection<OtherInformation> otherInformations = new ObservableCollection<OtherInformation>();

        public ObservableCollection<OtherInformation> OtherInformations
        {
            get { return otherInformations; }
            set { SetField(ref otherInformations, value); }
        }

        public ManagementViewModel()
        {
            maintenanceAgreements = TestData.GetMaintenanceAgreements();
            NotVisibleColumns.Add(nameof(ContactPartner.EMail));
            otherInformations = TestData.GetOtherInformations();
        }       

    }
}
