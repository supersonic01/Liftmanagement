using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            get
            {
               return otherInformations;
            }
            set { SetField(ref otherInformations, value); }
        }

        private List<OtherInformation> OtherInformationDeleted { get; set; } = new List<OtherInformation>();

        //private List<OtherInformation> otherInformations = new List<OtherInformation>();

        //public List<OtherInformation> OtherInformations
        //{
        //    get { return otherInformations; }
        //    set { SetField(ref otherInformations, value); }
        //}


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

        private MachineInformation machineInformationSelected = new MachineInformation();

        public MachineInformation MachineInformationSelected
        {
            get { return machineInformationSelected; }
            set
            {
                SetField(ref machineInformationSelected, value);
            }
        }


        //private OtherInformation otherInformationSelected = new OtherInformation();

        //public OtherInformation OtherInformationSelected
        //{
        //    get { return otherInformationSelected; }
        //    set
        //    {
        //        SetField(ref otherInformationSelected, value);
        //    }
        //}

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

            // maintenanceAgreements = TestData.GetMaintenanceAgreements();

            // Task.Factory.StartNew(() => RefreshOtherInformations());
            Task.Factory.StartNew(() => RefreshMaintenanceAgreements());
        }

        private void AddOtherInformationsDefault()
        {
            if (otherInformations.Count == 0)
            {
                OtherInformations.Add(new OtherInformation("Infomation 1"));
                OtherInformations.Add(new OtherInformation("Infomation 2"));
            }
        }

        private void RefreshOtherInformations()
        {
           // OtherInformations = new ObservableCollection<OtherInformation>(MySQLDataAccess.GetOtherInformations());
            AddOtherInformationsDefault();
        }

        private void RefreshMaintenanceAgreements()
        {
            MaintenanceAgreements = MySQLDataAccess.GetMaintenanceAgreements();
        }

        public void RefreshOtherInformations(MachineInformation machineInformation)
        {
            OtherInformations = new ObservableCollection<OtherInformation>(MySQLDataAccess.GetOtherInformations(machineInformation.Id));
            AddOtherInformationsDefault();
        }

        public void RefreshMaintenanceAgreements(MachineInformation machineInformation)
        {
            MaintenanceAgreements = MySQLDataAccess.GetMaintenanceAgreements(machineInformation.Id);
        }

        public void AddOtherInformations(MachineInformation machineInformation)
        {
            if (otherInformations.Count <= 0 || machineInformation== null && machineInformation.Id<0)
            {
                return;
            }
            MySQLDataAccess.AddOtherInformations(machineInformation, OtherInformations.ToList(), OtherInformationDeleted);
            OtherInformationDeleted= new List<OtherInformation>();
        }


        public void DeleteOtherInformation(OtherInformation otherInformationSelected)
        {
           OtherInformationDeleted.Add(otherInformationSelected);
        }
    }
}
