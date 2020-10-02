using Liftmanagement.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Data;
using Liftmanagement.Models;

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

        private Customer customerSelected = new Customer();

        public Customer CustomerSelected
        {
            get { return customerSelected; }
            set { SetField(ref customerSelected, value); }
        }

        private ContactPartner contactPerson = new ContactPartner();

        public ContactPartner ContactPerson 
        {
            get { return contactPerson; }
            set { SetField(ref contactPerson, value); }
        }

        public SQLQueryResult Add()
        {

         return  MySQLDataAccess.AddCustomer(CustomerSelected);

        }

     


    }
}
