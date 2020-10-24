using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using Liftmanagement.CollectionExtensions;
using Liftmanagement.Data;

namespace Liftmanagement.ViewModels
{
    public class MasterDataInfoViewModel : ViewModel
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
            set { SetField(ref customers, value); }
        }

        public ObservableCollection<MachineInformation> MachineInformations
        {
            get { return machineInformations; }
            set { SetField(ref machineInformations, value); }
        }

        private ObservableCollection<AdministratorCompany> administrators;

        public ObservableCollection<AdministratorCompany> Administrators
        {
            get
            {
                administrators = new ObservableCollection<AdministratorCompany>(customers.Select(c => c.Administrator));

                return administrators;
            }
        }

        private Customer customerSelected = new Customer();

        public Customer CustomerSelected
        {
            get { return customerSelected; }
            set
            {
                SetField(ref customerSelected, value);
                RefreshLocation();
            }
        }

        private Location locationSelected = new Location();

        public Location LocationSelected
        {
            get { return locationSelected; }
            set
            {
                SetField(ref locationSelected, value);
                RefreshMachineInformation();
            }
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

        public Helper.Helper.TTypeMangement MangementType { get; set; }

        public void RefreshCustomer()
        {
            customers.Clear();
            customers.Add(Customer.GetDummy());
            Customers = customers.AddRange(MySQLDataAccess.GetCustomers());
        }

        public void RefreshLocation()
        {
            if (customerSelected== null || customerSelected.Id == locationSelected.CustomerId)
                return;

            locations.Clear();
            if (MangementType != Helper.Helper.TTypeMangement.MachineInformation)
                locations.Add(Location.GetDummy());

            if (customerSelected.Id < -1)
            {
                Locations = locations.AddRange(MySQLDataAccess.GetLocations());
            }
            else
            {
                Locations = locations.AddRange(MySQLDataAccess.GetLocations(CustomerSelected));
            }
        }

        private void RefreshMachineInformation()
        {
            if (locationSelected==null || locationSelected.Id == machineInformationSelected.LocationId)
                return;

            machineInformations.Clear();
            if (locationSelected.Id < -1)
            {
                MachineInformations = machineInformations.AddRange(MySQLDataAccess.GetMachineInformations());
            }
            else
            {
                MachineInformations = machineInformations.AddRange(MySQLDataAccess.GetMachineInformations(locationSelected));
            }
        }

        public void RefreshLocation(Customer customer)
        {
            Locations = new ObservableCollection<Location>(MySQLDataAccess.GetLocations(customer));
        }

        public void RefreshLocationByCustomer(long id)
        {
            Locations = new ObservableCollection<Location>(MySQLDataAccess.GetLocations(id));
        }

    }
}
