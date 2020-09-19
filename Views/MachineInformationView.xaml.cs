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
    /// Interaction logic for MachineInformationView.xaml
    /// </summary>
    public partial class MachineInformationView : UserControl
    {
        public MachineInformationView()
        {
            InitializeComponent();

            var customersView = new CustomersView();
            //customersView.spCustomers.MinWidth = 50;
            frameCustomers.Content = customersView;

            var locationsView = new LocationsView();
            //locationsView.spLocations.MinWidth = 50;
            frameLocations.Content = locationsView;

            var machineInformationsView = new MachineInformationsView();
     
            machineInformationsView.NotVisibleColumns.Add("Description");
            machineInformationsView.NotVisibleColumns.Add("Entrances");
            frameMachineInformations.Content = machineInformationsView;
            machineInformationsView.dgMachineInformations.SelectionChanged += DgMachineInformations_SelectionChanged;
            machineInformationsView.dgMachineInformations.SelectedIndex = 0;


            customersView.expanderCustomers.Collapsed += ExpanderCustomers_Collapsed;
            customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;
           // customersView.spCustomers.Loaded += Stackpanel_Loaded;

            locationsView.expanderLocations.Collapsed += ExpanderLocations_Collapsed;
            locationsView.dgLocations.SelectionChanged += DgLocations_SelectionChanged;
            //locationsView.spLocations.Loaded += Stackpanel_Loaded;

            var masterDataInfo = new MasterDataInfoView();
            masterDataInfo.lblMachineInformation.Visibility = Visibility.Collapsed;
            masterDataInfo.cbMachineInformations.Visibility = Visibility.Collapsed;
            masterDataInfo.lblMachineInformationDetail.Visibility = Visibility.Collapsed;
            masterDataInfo.spMachineInformationDetailLbl.Visibility = Visibility.Collapsed;
            masterDataInfo.spMachineInformationDetail.Visibility = Visibility.Collapsed;

            frameMasterDataInfo.Content = masterDataInfo;


        }

        private void DgMachineInformations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MachineInformation machineInformation = null;
            var dgMachineInformations = sender as DataGrid;
            if (dgMachineInformations != null)
                machineInformation = dgMachineInformations.SelectedItem as MachineInformation;

            if (machineInformation == null)
            {
                return;

            }

            lblName.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Name)) + ":";
            lblYearOfConstruction.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.YearOfConstruction)) + ":";
            lblSerialNumber.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.SerialNumber)) + ":";
            lblHoldingPositions.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.HoldingPositions)) + ":";
            lblEntrances.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Entrances)) + ":";
            lblDescription.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Description)) + ":";
            lblContactPerson.Content = machineInformation.GetDisplayName<ContactPartner>(nameof(machineInformation.ContactPerson.Name)) + ":";
            lblPhoneWork.Content = machineInformation.GetDisplayName<ContactPartner>(nameof(machineInformation.ContactPerson.PhoneWork)) + ":";
            lblMobile.Content = machineInformation.GetDisplayName<ContactPartner>(nameof(machineInformation.ContactPerson.Mobile)) + ":";
            lblContactByDefect.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.ContactByDefect)) + ":";

            txtName.Text = machineInformation.Name;
            txtYearOfConstruction.Text = machineInformation.YearOfConstruction.ToString("dd.MM.yyyy");
            txtSerialNumber.Text = machineInformation.SerialNumber;
            txtHoldingPositions.Text = machineInformation.HoldingPositions.ToString();
            txtEntrances.Text = machineInformation.Entrances.ToString();
            txtDescription.Text = machineInformation.Description;
            txtContactPerson.Text = machineInformation.ContactPerson.Name;
            txtPhoneWork.Text = machineInformation.ContactPerson.PhoneWork;
            txtMobile.Text = machineInformation.ContactPerson.Mobile;
            cbContactByDefect.IsChecked = machineInformation.ContactByDefect;



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
            gridResizableMachineInformation.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Auto);
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


        }
        private void ExpanderCustomers_Collapsed(object sender, RoutedEventArgs e)
        {
            gridResizableMachineInformation.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }

        private void Stackpanel_Loaded(object sender, RoutedEventArgs e)
        {
            var stackpanel = sender as StackPanel;
            if (stackpanel == null)
                return;

            //Work around           
            stackpanel.Width = stackpanel.Width + 1;
            // stackpanel.Width = 450;
        }

    }
}
