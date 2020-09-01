using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Liftmanagement.View
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        public CustomerView()
        {
            InitializeComponent();
            gridCustomers.ItemsSource = TestData.GetCustomers(); ;
        }

        private void expanderCustomers_Expanded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(" is expanded");

        }

        private void spCustomers_Loaded(object sender, RoutedEventArgs e)
        {
            //Work around
            Console.WriteLine(string.Format("sp: {0} ", spCustomers.Width));
            spCustomers.Width = 451;
            spCustomers.Width = 450;
        }

        private void gridCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = gridCustomers.SelectedItem as Customer;
            if (customer != null)
            {
                Console.WriteLine("Customer :" + customer.Address);
            }
        }
    }
}
