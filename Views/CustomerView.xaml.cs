using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
            if (customer == null)
            {
                return;

            }

            Console.WriteLine("Customer :" + customer.Address);

            lblCompanyName.Content = customer.GetDisplayName<Customer>(nameof(customer.CompanyName)) + ":";
            lblContactPerson.Content = customer.GetDisplayName<Customer>(nameof(customer.ContactPerson)) + ":";
            lblAddress.Content = customer.GetDisplayName<Customer>(nameof(customer.Address)) + ":";
            lblPostcode.Content = customer.GetDisplayName<Customer>(nameof(customer.Postcode)) + ":";
            lblCity.Content = customer.GetDisplayName<Customer>(nameof(customer.City)) + ":";
            lblPhoneWork.Content = customer.GetDisplayName<Customer>(nameof(customer.PhoneWork)) + ":";
            lblMobile.Content = customer.GetDisplayName<Customer>(nameof(customer.Mobile)) + ":";

            txtCompanyName.Text = customer.CompanyName;
            txtContactPerson.Text = customer.ContactPerson;
            txtAddress.Text = customer.Address;
            txtPostcode.Text = customer.Postcode;
            txtCity.Text = customer.City;
            txtPhoneWork.Text = customer.PhoneWork;
            txtMobile.Text = customer.Mobile;


            List<Location> locations = TestData.GetLocations();

            var location = locations.Where(c => c.CustomerId == customer.CustomerId).FirstOrDefault();

            if (location == null)
            {
                return;
            }

            lblLocationAdditionalInfo.Content = location.GetDisplayName<Location>(nameof(location.AdditionalInfo)) + ":";
            lblLocationContactPerson.Content = location.GetDisplayName<Location>(nameof(location.ContactPerson)) + ":";
            lblLocationAddress.Content = location.GetDisplayName<Location>(nameof(location.Address)) + ":";
            lblLocationPostcode.Content = location.GetDisplayName<Location>(nameof(location.Postcode)) + ":";
            lblLocationCity.Content = location.GetDisplayName<Location>(nameof(location.City)) + ":";
            lblLocationPhoneWork.Content = location.GetDisplayName<Location>(nameof(location.PhoneWork)) + ":";
            lblLocationMobile.Content = location.GetDisplayName<Location>(nameof(location.Mobile)) + ":";

            txtLocationAdditionalInfo.Text = location.AdditionalInfo;
            txtLocationContactPerson.Text = location.ContactPerson;
            txtLocationAddress.Text = location.Address;
            txtLocationPostcode.Text = location.Postcode;
            txtLocationCity.Text = location.City;
            txtLocationPhoneWork.Text = location.PhoneWork;
            txtLocationMobile.Text = location.Mobile;

        }

        private void expanderCustomers_Collapsed(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("expanderCustomers_Collapsed" );
            gridResizableCustomers.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }
    }
}
