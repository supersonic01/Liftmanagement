using Liftmanagement.Converters;
using Liftmanagement.Models;
using Liftmanagement.Reports;
using Liftmanagement.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : UserControlView
    {
        private CustomersView customersView;
        private LocationsView locationsView;
        private MachineInformationsView machineInfosView;
        private MasterDataInfoView masterDataInfo;

        public ReportViewModel ReportVM { get; set; } = new ReportViewModel();

        public ReportView()
        {
            InitializeComponent();

            var customersVM = new CustomersViewModel();
            customersVM.RefreshOnlyCustomers();
            customersView = new CustomersView();
            customersView.CustomersVM = customersVM;
            frameCustomers.Content = customersView;

            locationsView = new LocationsView();
            frameLocations.Content = locationsView;

            var machineInformationsView = new MachineInformationsView();
            machineInformationsView.NotVisibleColumns.Add(nameof(MachineInformation.GoogleDriveFolderName));
            machineInformationsView.NotVisibleColumns.Add(nameof(MachineInformation.ContactPerson));
            machineInformationsView.NotVisibleColumns.Add(nameof(MachineInformation.Description));
            machineInformationsView.NotVisibleColumns.Add(nameof(MachineInformation.Payload));
            machineInformationsView.NotVisibleColumns.Add(nameof(MachineInformation.Entrances));
            machineInformationsView.NotVisibleColumns.Add(nameof(MachineInformation.HoldingPositions));
            machineInformationsView.NotVisibleColumns.Add(nameof(MachineInformation.HoldingPositions));
            frameMachineInformations.Content = machineInformationsView;

            masterDataInfo = new MasterDataInfoView(Helper.Helper.TTypeMangement.MaintenanceAgreement);
            masterDataInfo.cbMachineInformations.SelectionChanged += CbMachineInformations_SelectionChanged;

            masterDataInfo.RefreshCustomers();
            frameMasterDataInfo.Content = masterDataInfo;

            customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;

            locationsView.dgLocations.SelectionChanged += DgLocations_SelectionChanged;

            machineInformationsView.dgMachineInformations.SelectionChanged += DgMachineInformations_SelectionChanged;

            BindingControl(dgReports, () => ReportVM.Records);
            BindingTextBlock(txtOfferPrice, () => ReportVM.OfferPriceSum, null, "c");
            BindingTextBlock(txtBillingAmountCorrect, () => ReportVM.BillingAmountCorrecteSum, null, "c");
            BindingTextBlock(txtSavingCost, () => ReportVM.SavingCostSum, null, "c");

            var binding = new Binding(GetPropertyPath(() => ReportVM.SavingCostSum))
            {
                Source = this,
            };

            binding.Mode = BindingMode.TwoWay;
            binding.Converter = new ValueToForegroundColorConverter();

            txtSavingCost.SetBinding(TextBlock.ForegroundProperty, binding);

            BindingComboBoxBindingModeOneWay(cbYears, () => ReportVM.Years);
            BindingComboBoxSelectedValue(cbYears, () => ReportVM.YearSelected);

            var rbbinding = new Binding(GetPropertyPath(() => ReportVM.YearChecked))
            {
                Source = this,
            };

            rbbinding.Mode = BindingMode.TwoWay;

            rbYear.SetBinding(RadioButton.IsCheckedProperty, rbbinding);

            BindingDatePicker(datePickerDurationStart, () => ReportVM.ReportStart);
            BindingDatePicker(datePickerDurationEnd, () => ReportVM.ReportEnd);

        }




        private void CbMachineInformations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var machineInformation = GetSelectedObject<MachineInformation>(sender);

            if (machineInformation != null)
            {
                ReportVM.RefreshMachine(machineInformation);
                cbYears.SelectedIndex = 0;
            }
        }


        private void DgLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var location = GetSelectedObject<Location>(sender);

            if (location != null)
            {

                masterDataInfo.MasterDataInfoVM.RefreshLocationByCustomer(location.CustomerId);

                masterDataInfo.cbLocations.SelectedItem = masterDataInfo.cbLocations.Items.Cast<Location>()
                    .Single(c => c.Id == location.Id);

                customersView.dgCustomers.SelectedItem = customersView.dgCustomers.Items.Cast<Customer>()
                    .Single(c => c.Id == location.CustomerId);

            }
        }

        private void DgMachineInformations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var machineInformation = GetSelectedObject<MachineInformation>(sender);

            if (machineInformation != null)
            {
                masterDataInfo.cbMachineInformations.SelectedItem = masterDataInfo.cbMachineInformations.Items
                    .Cast<MachineInformation>()
                    .Single(c => c.Id == machineInformation.Id);
            }
        }
        private void ExpanderLocations_Collapsed(object sender, RoutedEventArgs e)
        {
            gridResizableMaintenanceAgreements.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);
        }

        private void DgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = GetSelectedObject<Customer>(sender);

            if (customer != null)
                masterDataInfo.cbCustomers.SelectedItem = masterDataInfo.cbCustomers.Items.Cast<Customer>().Single(c => c.Id == customer.Id);

        }
        private void ExpanderCustomers_Collapsed(object sender, RoutedEventArgs e)
        {
            gridResizableMaintenanceAgreements.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }

        private void datePickerDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void btnShowReport_Click(object sender, RoutedEventArgs e)
        {
            ReportVM.Refresh();

        }

        private void btnPrintReport_Click(object sender, RoutedEventArgs e)
        {
            ReportVM.Refresh();
            var report = new CostSavingReport(ReportVM.Records, 
                masterDataInfo.MasterDataInfoVM.CustomerSelected,
                masterDataInfo.MasterDataInfoVM.LocationSelected,
                masterDataInfo.MasterDataInfoVM.MachineInformationSelected);
        }

        private void RbYear_OnChecked(object sender, RoutedEventArgs e)
        {
            EnableTimeRange(sender);
        }

        private void EnableTimeRange(object sender, bool correctionValue= true)
        {
            RadioButton rb = sender as RadioButton;
            if (rb == null || datePickerDurationEnd == null || datePickerDurationStart == null || cbYears == null)
            {
                return;
            }

            bool newBool = rb.IsChecked.HasValue ? rb.IsChecked.Value : false;

            datePickerDurationEnd.IsEnabled = !(newBool& correctionValue);
            datePickerDurationStart.IsEnabled = !(newBool& correctionValue);
            cbYears.IsEnabled = (newBool&correctionValue);
        }

        private void RbTimeRange_OnChecked(object sender, RoutedEventArgs e)
        {
            EnableTimeRange(sender,false);
        }
    }
}
