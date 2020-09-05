using Liftmanagement.Helper;
using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using Liftmanagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public CustomerViewModel CustomerVM { get; set; } = new CustomerViewModel();

        public CustomerView()
        {
            InitializeComponent();

            var customersView = new CustomersView();
            frameCustomers.Content = customersView;
            customersView.expanderCustomers.Collapsed += ExpanderCustomers_Collapsed;
            customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;
            customersView.spCustomers.Loaded += SpCustomers_Loaded;

            //Binding binding = new Binding("CustomerVM.LocationDetailViews")
            //{
            //    Source = this
            //};

            //gridLocation.SetBinding(ListBox.ItemsSourceProperty, binding);

        }

        private void SpCustomers_Loaded(object sender, RoutedEventArgs e)
        {
            var spCustomers = sender as StackPanel;
            if (spCustomers == null)
                return;

            //Work around
            Console.WriteLine(string.Format("sp: {0} ", spCustomers.Width));
            spCustomers.Width = 451;
            spCustomers.Width = 450;
        }

        private void DgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = null;
            var dgCustomers = sender as DataGrid;
            if (dgCustomers != null)
                customer = dgCustomers.SelectedItem as Customer;

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

            //  var location = locations.Where(c => c.CustomerId == customer.CustomerId).FirstOrDefault();

              ObservableCollection<LocationDetailView> locationDetailViews = new ObservableCollection<LocationDetailView>();
        locations.ForEach(c => locationDetailViews.Add(new LocationDetailView(c)));
            locations.ForEach(c => locationDetailViews.Add(new LocationDetailView(c)));
            //  CustomerVM.LocationDetailViews = locationDetailViews;
            //gridLocation.ItemsSource = CustomerVM.LocationDetailViews;

            DtContactDetailListView.ItemsSource = locationDetailViews;

        }


        private void ExpanderCustomers_Collapsed(object sender, RoutedEventArgs e)
        {

            Console.WriteLine("expanderCustomers_Collapsed");
            gridResizableCustomers.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }
    }
}
