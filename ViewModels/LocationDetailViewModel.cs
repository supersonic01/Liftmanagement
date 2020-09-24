using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
  public class LocationDetailViewModel: ViewModel
    {
        private Location location;

        public Location Location
        {
            get { return location; }
            set { SetField(ref location, value); }
        }
    }
}
