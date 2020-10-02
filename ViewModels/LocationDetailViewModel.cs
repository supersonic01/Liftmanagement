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
  public class LocationDetailViewModel: ViewModel
    {
        private Location locationSelected = new Location();

        public Location LocationSelected
        {
            get { return locationSelected; }
            set { SetField(ref locationSelected, value); }
        }

        public SQLQueryResult Add(Customer customer)
        {

            return null;
        }

    }
}
