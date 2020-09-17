using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            locations = TestData.GetLocations();
        }

        public void LocationsByCustomer(Customer customer)
        {
            Locations= TestData.GetLocations().Where(c=>c.CustomerId == customer.Id).ToList();
        }
    
    }
}
