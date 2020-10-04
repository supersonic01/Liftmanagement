using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Data;

namespace Liftmanagement.ViewModels
{
   public class CustomersViewModel : ViewModel
    {

        private List<Customer> customers = new List<Customer>();

        public List<Customer> Customers
        {
            get { return customers; }
            set { SetField(ref customers, value); }
        }


        public CustomersViewModel()
        {
            RefreshCustomers();
        }

        public void RefreshCustomers()
        {
           // Customers = TestData.GetCustomers();
           Customers = MySQLDataAccess.GetCustomers();

          var dd = new Customer();
       


        }

       

    }
}
