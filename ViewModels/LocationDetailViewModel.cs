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
            locationSelected.ReleaseRow();
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

        public SQLQueryResult<Location> ForceEditing()
        {
            return MySQLDataAccess.ForceToEditLocation(LocationSelected.Id);
        }

        public void ReleaseEditing()
        {
            Task.Factory.StartNew(() =>
            {
                MySQLDataAccess.ReleaseEditingLocation(LocationSelected.Id);
            });
        }

        public SQLQueryResult<Location> MarkForDeleteCustomer()
        {
            return MySQLDataAccess.MarkForDeleteLocation(LocationSelected);
        }
    }
}
