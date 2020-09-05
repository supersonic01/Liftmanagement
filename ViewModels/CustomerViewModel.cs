using Liftmanagement.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.ViewModels
{
   public class CustomerViewModel : ViewModel
    {
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { SetField(ref myVar, value); }
        }


        private List<LocationDetailView> locationDetailViews = new List<LocationDetailView>();

        public List<LocationDetailView> LocationDetailViews
        {
            get { return locationDetailViews; }
            set { SetField(ref locationDetailViews, value); }
        }

    }
}
