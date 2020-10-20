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
        private List<Location> locations = new List<Location>();
        private List<MachineInformation> machineInformations = new List<MachineInformation>();

        public List<Location> Locations
        {
            get { return locations; }
            set { SetField(ref locations, value); }
        }


        //public List<Customer> Customers
        //{
        //    get { return customers; }
        //    set { SetField(ref customers, value); }
        //}

        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set { SetField(ref customers, value); }
        }



        public List<MachineInformation> MachineInformations
        {
            get { return machineInformations; }
            set { SetField(ref machineInformations, value); }
        }

        private List<AdministratorCompany> administrators;

        public List<AdministratorCompany> Administrators
        {
            get
            {
                administrators = new List<AdministratorCompany>(customers.Select(c => c.Administrator));

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
            set { SetField(ref locationSelected, value); }
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
            customers.Add(Customer.GetDummy());
            Customers= customers.AddRange(MySQLDataAccess.GetCustomers());
        }

     

        public void RefreshLocation()
        {
            if(customerSelected.Id == locationSelected.CustomerId)
                return;

            if (CustomerSelected.Id < 0)
            {
                Locations = MySQLDataAccess.GetLocations();
            }
            else
            {
                Locations = MySQLDataAccess.GetLocations(CustomerSelected);
            }
        }

        public void RefreshLocation(Customer customer)
        {
            Locations = MySQLDataAccess.GetLocations(customer);
        }

        public void RefreshLocationByCustomer(long id)
        {
            Locations = MySQLDataAccess.GetLocations(id);
        }

    }
}
