using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Liftmanagement.CollectionExtensions;
using Liftmanagement.Data;

namespace Liftmanagement.ViewModels
{
    public class ManagementViewModel : ViewModel
    {
        private ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
        private ObservableCollection<Location> locations = new ObservableCollection<Location>();
        private ObservableCollection<MachineInformation> machineInformations = new ObservableCollection<MachineInformation>();
        private List<MaintenanceAgreement> maintenanceAgreements = new List<MaintenanceAgreement>();
        private ObservableCollection<OtherInformation> otherInformations = new ObservableCollection<OtherInformation>();
        private ObservableCollection<AdministratorCompany> administrators;

        public List<MaintenanceAgreement> MaintenanceAgreements
        {
            get { return maintenanceAgreements; }
            set { SetField(ref maintenanceAgreements, value); }
        }

        public ObservableCollection<OtherInformation> OtherInformations
        {
            get { return otherInformations; }
            set { SetField(ref otherInformations, value); }
        }

        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set { SetField(ref locations, value); }
        }

        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set
            {
                SetField(ref customers, value);
                Administrators = new ObservableCollection<AdministratorCompany>(customers.Select(c => c.Administrator));
            }
        }

        public ObservableCollection<MachineInformation> MachineInformations
        {
            get { return machineInformations; }
            set { SetField(ref machineInformations, value); }
        }


        public ObservableCollection<AdministratorCompany> Administrators
        {
            get { return administrators; }
            private set { SetField(ref administrators, value); }
        }

        public ManagementViewModel()
        {

        }

        public void Refresh()
        {
            var customerTask = Task.Factory.StartNew(() =>
              {
                  Customers = new ObservableCollection<Customer>(MySQLDataAccess.GetCustomers());
              });

            var locationTask = Task.Factory.StartNew(() =>
            {
                Locations = new ObservableCollection<Location>(MySQLDataAccess.GetLocations());
            });
            var machineInfomationTask = Task.Factory.StartNew(() =>
            {
                MachineInformations = new ObservableCollection<MachineInformation>(MySQLDataAccess.GetMachineInformations());
            });

            maintenanceAgreements = TestData.GetMaintenanceAgreements();
            otherInformations = TestData.GetOtherInformations();

        }



    }
}
