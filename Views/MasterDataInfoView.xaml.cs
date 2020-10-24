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
using Liftmanagement.Converters;
using LocalDataStoreSlot = System.LocalDataStoreSlot;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for MasterDataInfoView.xaml
    /// </summary>
    public partial class MasterDataInfoView : UserControlView
    {
        public MasterDataInfoViewModel MasterDataInfoVM { get; set; } = new MasterDataInfoViewModel();

        public MasterDataInfoView(Helper.Helper.TTypeMangement mangementType)
        {
            InitializeComponent();
            MasterDataInfoVM.MangementType = mangementType;
            AssigneValuesToControl();

            cbCustomers.SelectionChanged += CbCustomers_SelectionChanged;
            cbLocations.SelectionChanged += CbLocations_SelectionChanged;
            cbMachineInformations.SelectionChanged += CbMachineInformations_SelectionChanged;

            if (mangementType == Helper.Helper.TTypeMangement.MachineInformation)
                CollapsedMachineInformations();

           
        }

        public void RefreshCustomers()
        {
            MasterDataInfoVM.RefreshCustomer();
            cbCustomers.SelectedIndex = 1;
        }

        public MasterDataInfoView()
        {
        }

        private void CollapsedMachineInformations()
        {
            lblMachineInformation.Visibility = Visibility.Collapsed;
            cbMachineInformations.Visibility = Visibility.Collapsed;
            lblMachineInformationDetail.Visibility = Visibility.Collapsed;
            spMachineInformationDetailLbl.Visibility = Visibility.Collapsed;
            spMachineInformationDetail.Visibility = Visibility.Collapsed;

            cbMachineInformations.SelectionChanged += CbMachineInformations_SelectionChanged;
            //cbMachineInformations.SelectedIndex = 0;
        }

        private void AssigneValuesToControl()
        {

            BindingLabel(lblCompanyName, () => MasterDataInfoVM.CustomerSelected.CompanyName);
            BindingLabel(lblAdministratorCompanyName, () => MasterDataInfoVM.CustomerSelected.Administrator.Name);
            BindingLabel(lblAddress, () => MasterDataInfoVM.CustomerSelected.Address);
            MultiBindingLabel(lblPostcodeCity, () => MasterDataInfoVM.CustomerSelected.Postcode, () => MasterDataInfoVM.CustomerSelected.City);

            BindingLabel(lblAddressLocation, () => MasterDataInfoVM.LocationSelected.Address);
            MultiBindingLabel(lblPostcodeCityLocation, () => MasterDataInfoVM.LocationSelected.Postcode, () => MasterDataInfoVM.LocationSelected.City);
            BindingLabel(lblContactPersonLocation, () => MasterDataInfoVM.LocationSelected.ContactPerson.Name);

            lblMachineNameHeader.Content = MasterDataInfoVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MasterDataInfoVM.MachineInformationSelected.Name));
            lblSerialNumberHeader.Content = MasterDataInfoVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MasterDataInfoVM.MachineInformationSelected.SerialNumber));
            lblYearOfConstructionHeader.Content = MasterDataInfoVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MasterDataInfoVM.MachineInformationSelected.YearOfConstruction));
            lblHoldingPositionsHeader.Content = MasterDataInfoVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MasterDataInfoVM.MachineInformationSelected.HoldingPositions));
            lblEntrancesHeader.Content = MasterDataInfoVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MasterDataInfoVM.MachineInformationSelected.Entrances));
            lblPayloadHeader.Content = MasterDataInfoVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MasterDataInfoVM.MachineInformationSelected.Payload));

            BindingLabel(lblMachineName, () => MasterDataInfoVM.MachineInformationSelected.Name);
            BindingLabel(lblSerialNumber, () => MasterDataInfoVM.MachineInformationSelected.SerialNumber);
            BindingLabel(lblYearOfConstruction, () => MasterDataInfoVM.MachineInformationSelected.YearOfConstruction);
            BindingLabel(lblHoldingPositions, () => MasterDataInfoVM.MachineInformationSelected.HoldingPositions);
            BindingLabel(lblEntrances, () => MasterDataInfoVM.MachineInformationSelected.Entrances);
            BindingLabel(lblPayload, () => MasterDataInfoVM.MachineInformationSelected.Payload);

            BindingControl(cbCustomers, () => MasterDataInfoVM.Customers);
            BindingControl(cbLocations, () => MasterDataInfoVM.Locations);
            BindingControl(cbMachineInformations, () => MasterDataInfoVM.MachineInformations);

        }

        private void CbMachineInformations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MasterDataInfoVM.MachineInformationSelected = GetSelectedObject<MachineInformation>(sender);
        }

        private void CbLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MasterDataInfoVM.LocationSelected = GetSelectedObject<Location>(sender);
            cbMachineInformations.SelectedIndex = 0;
        }

        private void CbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MasterDataInfoVM.CustomerSelected = GetSelectedObject<Customer>(sender);

            var location = cbLocations.SelectedItem as Location;
          
            if (location == null || MasterDataInfoVM.CustomerSelected.Id != location.CustomerId)
            {
                cbLocations.SelectedIndex = MasterDataInfoVM.MangementType == Helper.Helper.TTypeMangement.MachineInformation ? 0 : 1;
            }

        }

       
    }
}
