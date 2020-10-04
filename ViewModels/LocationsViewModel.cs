using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Data;

namespace Liftmanagement.ViewModels
{
   public class LocationsViewModel : ViewModel
    {

        private List<Location> locations = new List<Location>();

        public List<Location> Locations
        {
            get { return locations; }
            set { SetField(ref locations, value); }
        }


        public LocationsViewModel()
        {
            RefreshCustomers();
           
        }

        public void RefreshCustomers()
        {
            Locations = MySQLDataAccess.GetLocations();
        }

        public void LocationsByCustomer(Customer customer)
        {
            // Locations= TestData.GetLocations().Where(c=>c.CustomerId == customer.Id).ToList();

            Locations = TestData.GetLocations().Where(c => c.CustomerId == customer.Id).ToList();
        }

        public List<Location> GetLocationsByCustomer(Customer customer)
        {
            //return TestData.GetLocations().Where(c => c.CustomerId == customer.Id).ToList();
            return Locations.Where(c => c.CustomerId == customer.Id).ToList();
        }
    }
}
