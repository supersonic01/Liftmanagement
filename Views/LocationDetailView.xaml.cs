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
using Liftmanagement.ViewModels;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for LocationDetailView.xaml
    /// </summary>
    public partial class LocationDetailView : GoogleDriveDialogueView
    {
        public LocationDetailViewModel LocationDetailVM { get; set; } = new LocationDetailViewModel();

        protected override string ViewModelName => nameof(LocationDetailVM);
        protected override string SourceObjectStringName => nameof(LocationDetailVM.Location);

        public LocationDetailView(Location location)
        {
            InitializeComponent();

            if (location == null)
            {
                return;
            }

            lblAdditionalInfo.Content = location.GetDisplayName<Location>(nameof(location.AdditionalInfo)) + ":";
            lblContactPerson.Content = location.GetDisplayName<ContactPartner>(nameof(location.ContactPerson.Name)) + ":";
            lblAddress.Content = location.GetDisplayName<Location>(nameof(location.Address)) + ":";
            lblPostcode.Content = location.GetDisplayName<Location>(nameof(location.Postcode)) + ":";
            lblCity.Content = location.GetDisplayName<Location>(nameof(location.City)) + ":";
            lblPhoneWork.Content = location.GetDisplayName<ContactPartner>(nameof(location.ContactPerson.PhoneWork)) + ":";
            lblMobile.Content = location.GetDisplayName<ContactPartner>(nameof(location.ContactPerson.Mobile)) + ":";
            lblEmail.Content = location.GetDisplayName<ContactPartner>(nameof(location.ContactPerson.EMail)) + ":";
            lblContactByDefect.Content = location.GetDisplayName<Location>(nameof(location.ContactByDefect)) + ":";
            lblGoogleDriveLink.Content = location.GetDisplayName<Location>(nameof(location.GoogleDriveLink)) + ":";

            DataContext = location;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO Set Titel
            //var customer = customersView.dgCustomers.SelectedItem as Customer;
            //if (customer != null)
            //{
            //    windowGoogleDriveTree.Title = windowGoogleDriveTree.Title + "  " + customer.CompanyName;
            //}
            windowGoogleDriveTree.ShowDialog();
        }
        
    }
}
