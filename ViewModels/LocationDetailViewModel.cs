using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Data;
using Liftmanagement.Helper;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
    public class LocationDetailViewModel : ViewModel
    {
        private Location locationSelected = new Location();

        public Location LocationSelected
        {
            get { return locationSelected; }
            set { SetField(ref locationSelected, value); }
        }

        public SQLQueryResult<Location> Add(Customer customer)
        {
            if (locationSelected.Id > 0)
            {
                return MySQLDataAccess.UpdateLocation(locationSelected);
            }
            else
            {
                locationSelected.CustomerId = customer.Id;
                return MySQLDataAccess.AddLocation(locationSelected);
            }
        }

        public SQLQueryResult<Location> EditLocation()
        {
            var result = MySQLDataAccess.GetLocationForEdit(LocationSelected.Id);
            if (!result.IsReadOnly)
            {
                LocationSelected = result.DBRecords.FirstOrDefault() as Location;
            }

            return result;
        }

    }
}
