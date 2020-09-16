using Liftmanagement.ViewModels;
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
    /// Interaction logic for LocationsView.xaml
    /// </summary>
    public partial class LocationsView : UserControl
    {
        public LocationsViewModel LocationsVM { get; set; } = new LocationsViewModel();
        public LocationsView()
        {
            InitializeComponent();
            //  dgLocations.ItemsSource = LocationsVM.Locations; ;
            
            Binding binding = new Binding("LocationsVM.Locations")
            {
                Source = this
            };

            dgLocations.SetBinding(DataGrid.ItemsSourceProperty, binding);

            txtExpanderHeader.Text = new CategoryViewModel().Categories.Where(c => c.MangementType == Helper.Helper.TTypeMangement.Location).Select(c => c.Name).FirstOrDefault();
            expanderLocations.Expanded += ExpanderLocations_Expanded;
        }

        private void ExpanderLocations_Expanded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(spLocations.ActualWidth);
            spLocations.Width = spLocations.ActualWidth;
        }
    }
}
