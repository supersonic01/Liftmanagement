using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.ViewModels
{
  public  class MasterDataInfoViewModel : ViewModel
    {
       
        private List<Customer> customers = new List<Customer>();
        private List<Location> locations = new List<Location>();
        private List<MachineInformation> machineInformations = new List<MachineInformation>();

        public List<Location> Locations
        {
            get { return locations; }
            set { SetField(ref locations, value); }
        }


        public List<Customer> Customers
        {
            get { return customers; }
            set { customers = value; }
        }


        public List<MachineInformation> MachineInformations
        {
            get { return machineInformations; }
            set { machineInformations = value; }
        }


        public MasterDataInfoViewModel()
        {
            customers = TestData.GetCustomers();
            locations = TestData.GetLocations();
            machineInformations = TestData.GetMachineInformations();
        }

    }
}
