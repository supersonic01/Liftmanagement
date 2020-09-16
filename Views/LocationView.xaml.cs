using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for LocationView.xaml
    /// </summary>
    public partial class LocationView : UserControl
    {
        private LocationsView locationsView;

        public LocationView()
        {
            InitializeComponent();

            var customersView = new CustomersView();
            frameCustomers.Content = customersView;

            locationsView = new LocationsView();
            frameLocations.Content = locationsView;

            customersView.expanderCustomers.Collapsed += ExpanderCustomers_Collapsed;
            customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;
            customersView.spCustomers.Loaded += Stackpanel_Loaded;

            locationsView.expanderLocations.Collapsed += ExpanderLocations_Collapsed;
            locationsView.dgLocations.SelectionChanged += DgLocations_SelectionChanged;
            locationsView.spLocations.Loaded += Stackpanel_Loaded;
        }

       

        private void DgLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Location location = null;
            var dgLogations = sender as DataGrid;
            if (dgLogations != null)
                location = dgLogations.SelectedItem as Location;

            if (location == null)
            {
                return;

            }

            Console.WriteLine("Customer :" + location.Address);
        }

        private void ExpanderLocations_Collapsed(object sender, RoutedEventArgs e)
        {
            gridResizableLocations.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Auto);
        }

        private void Stackpanel_Loaded(object sender, RoutedEventArgs e)
        {
            var stackpanel = sender as StackPanel;
            if (stackpanel == null)
                return;

            //Work around           
            stackpanel.Width = 451;
            stackpanel.Width = 450;
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

            locationsView.LocationsVM.LocationsByCustomer(customer);

        }
        private void ExpanderCustomers_Collapsed(object sender, RoutedEventArgs e)
        {
            gridResizableLocations.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }
    }
}
