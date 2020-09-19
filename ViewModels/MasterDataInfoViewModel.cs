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
  public  class MasterDataInfoViewModel : ViewModel
    {
       
        private ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
        private ObservableCollection<Location> locations = new ObservableCollection<Location>();
        private ObservableCollection<MachineInformation> machineInformations = new ObservableCollection<MachineInformation>();

        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set { SetField(ref locations, value); }
        }


        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set { customers = value; }
        }


        public ObservableCollection<MachineInformation> MachineInformations
        {
            get { return machineInformations; }
            set { machineInformations = value; }
        }

        private ObservableCollection<AdministratorCompany> administrators;

        public ObservableCollection<AdministratorCompany> Administrators
        {
            get
            {
                administrators = new ObservableCollection<AdministratorCompany> (customers.Select(c => c.Administrator));

                return administrators;
            }
        }

        public MasterDataInfoViewModel()
        {
            customers = new ObservableCollection<Customer> (TestData.GetCustomers());
            locations = new ObservableCollection<Location>(TestData.GetLocations());
            machineInformations = new ObservableCollection<MachineInformation>(TestData.GetMachineInformations());
        }

    }
}
