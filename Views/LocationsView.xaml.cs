using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using System.Linq;
using System.Windows;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for LocationsView.xaml
    /// </summary>
    public partial class LocationsView : UserControlView
    {
        public LocationsViewModel LocationsVM { get; set; } = new LocationsViewModel();
        public LocationsView()
        {
            InitializeComponent();

            //Task.Factory.StartNew(() =>
            //{
            //    LocationsVM.Refresh();
            //});
            LocationsVM.Refresh();
            NotVisibleColumns.Add(nameof(Location.GoogleDriveFolderName));

            dgLocations.Tag = NotVisibleColumns;

            BindingControl(dgLocations, () => LocationsVM.Locations);

            //txtExpanderHeader.Text = new CategoryViewModel().Categories.Where(c => c.MangementType == Helper.Helper.TTypeMangement.Location).Select(c => c.Name).FirstOrDefault();
            txtExpanderHeader.Text = "Standorte";
            expanderLocations.Expanded += ExpanderLocations_Expanded;
            expanderLocations.Collapsed += ExpanderLocations_Collapsed;
        }
             

        public void ExpanderLocations_Expanded(object sender, RoutedEventArgs e)
        {
            dgLocations.Width = dgLocations.ActualWidth;
        }               

        private void ExpanderLocations_Collapsed(object sender, RoutedEventArgs e)
        {
            gridLocations.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }

        private void gspLocatios_OnDragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            dgLocations.Width = double.NaN;
        }
    }
}
