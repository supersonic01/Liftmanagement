using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.CollectionExtensions;
using Liftmanagement.Data;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
    public class MaintenanceAgreementContentViewModel : ViewModel
    {
        private ObservableCollection<MaintenanceAgreementContent> maintenanceAgreementContents = new ObservableCollection<MaintenanceAgreementContent>();

        public ObservableCollection<MaintenanceAgreementContent> MaintenanceAgreementContents
        {
            get { return maintenanceAgreementContents; }
            set { SetField(ref maintenanceAgreementContents, value); }
        }

        private ObservableCollection<string> provides = new ObservableCollection<string>{"Ja","Nein"};

        public ObservableCollection<string> Provides
        {
            get { return provides; }
            set { SetField(ref provides, value); }
        }



        public void Refresh(long maintenanceAgreementId)
        {
            if (maintenanceAgreementId < 0)
            {
                List<MaintenanceAgreementContent> dd = new List<MaintenanceAgreementContent>();
                dd.Add(new MaintenanceAgreementContent { Content = "Inhalt 1", Provide = "Nein" });
                dd.Add(new MaintenanceAgreementContent { Content = "Inhalt 2", Provide = "Ja" });
                MaintenanceAgreementContents = new ObservableCollection<MaintenanceAgreementContent>(dd);
                return;
            }
            
             MaintenanceAgreementContents = new ObservableCollection<MaintenanceAgreementContent>(MySQLDataAccess.GetMaintenanceAgreementContents(maintenanceAgreementId));

        }

    }
}
