using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.ViewModels
{
   public class CustomersViewModel : ViewModel
    {

        private List<Customer> customers = new List<Customer>();

        public List<Customer> Customers
        {
            get { return customers; }
            set { customers = value; }
        }


        public CustomersViewModel()
        {
            customers = TestData.GetCustomers();
        }

       

    }
}
