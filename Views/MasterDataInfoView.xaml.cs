using Liftmanagement.Models;
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
    /// Interaction logic for MasterDataInfoView.xaml
    /// </summary>
    public partial class MasterDataInfoView : UserControl
    {
        MasterDataInfoViewModel masterDataInfoViewModel = new MasterDataInfoViewModel();
        public MasterDataInfoView()
        {
            InitializeComponent();

            cbCustomers.ItemsSource = masterDataInfoViewModel.Customers;
            cbLocations.ItemsSource = masterDataInfoViewModel.Locations;
            cbMachineInformations.ItemsSource = masterDataInfoViewModel.MachineInformations;

           

            cbCustomers.SelectionChanged += CbCustomers_SelectionChanged;
            cbLocations.SelectionChanged += CbLocations_SelectionChanged;
            cbMachineInformations.SelectionChanged += CbMachineInformations_SelectionChanged;

            cbCustomers.SelectedIndex = 0;
            cbLocations.SelectedIndex = 0;
            cbMachineInformations.SelectedIndex = 0;


        }

        private void CbMachineInformations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MachineInformation machineInformation = (sender as ComboBox).SelectedItem as MachineInformation;

            if (machineInformation == null)
            {
                return;
            }

            lblMachineNameHeader.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Name));
            lblSerialNumberHeader.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.SerialNumber));
            lblYearOfConstructionHeader.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.YearOfConstruction));
            lblHoldingPositionsHeader.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.HoldingPositions));
            lblEntrancesHeader.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Entrances));
            lblPayloadHeader.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Payload));

            lblMachineName.Content = machineInformation.Name;
            lblSerialNumber.Content = machineInformation.SerialNumber;
            lblYearOfConstruction.Content = machineInformation.YearOfConstruction;
            lblHoldingPositions.Content = machineInformation.HoldingPositions;
            lblEntrances.Content = machineInformation.Entrances;
            lblPayload.Content = machineInformation.Payload;
        }

        private void CbLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Location location = (sender as ComboBox).SelectedItem as Location;

            if (location == null)
            {
                return;
            }

            cbMachineInformations.ItemsSource = masterDataInfoViewModel.MachineInformations.Where(c => c.LocationId == location.Id).ToList();

            lblAddressLocation.Content = location.Address;
            lblPostcodeCityLocation.Content = location.Postcode + ", " + location.City;
            lblAdditionalInfoLocation.Content = location.AdditionalInfo;
            lblContactPersonLocation.Content = location.ContactPerson;

            if (string.IsNullOrWhiteSpace(location.AdditionalInfo))
            {
                lblAdditionalInfoLocation.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(location.ContactPerson.Name))
            {
                lblContactPersonLocation.Visibility = Visibility.Collapsed;
            }

            cbMachineInformations.SelectedIndex = 0;


        }

        private void CbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = (sender as ComboBox).SelectedItem as Customer;

            if (customer == null)
            {
                return;
            }

            cbLocations.ItemsSource = masterDataInfoViewModel.Locations.Where(c => c.CustomerId == customer.Id).ToList();

            lblCompanyName.Content = customer.CompanyName;

            if (string.IsNullOrWhiteSpace(customer.AdditionalInfo))
            {
                lblAdditionalInfo.Visibility = Visibility.Collapsed;

            }

            if (string.IsNullOrWhiteSpace(customer.ContactPerson.Name))
            {

                lblContactPerson.Visibility = Visibility.Collapsed;
            }
            lblAdditionalInfo.Content = customer.AdditionalInfo;
            lblContactPerson.Content = customer.ContactPerson;
            lblAddress.Content = customer.Address;
            lblPostcodeCity.Content = customer.Postcode + ", " + customer.City;

            cbLocations.SelectedIndex = 0;
            cbMachineInformations.SelectedIndex = 0;

        }
    }
}
