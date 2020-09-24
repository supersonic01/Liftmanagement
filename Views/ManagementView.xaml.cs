using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ManagementView.xaml
    /// </summary>
    public partial class ManagementView : UserControl
    {
        private ManagementViewModel managementVM = new ManagementViewModel();
        private Window overviewFilterViewWindow;
        private ManagementOverviewFilterView overviewFilterView;
        private RecordingsView recordingsView;
        private RecordingView recordingView;
        private Window recordingViewWindow;

        public ManagementViewModel ManagementVM
        {
            get { return managementVM; }
            set { managementVM = value; }
        }

        public ManagementView()
        {
            InitializeComponent();
            recordingsView = new RecordingsView();
            frameRecordings.Content = recordingsView;

            lblCustomerHeader.Content = "Rechnungsadresse:";
            lblLocationHeader.Content = "Standort:";
            lblMaintenanceIntervalHeader.Content = "Wartungsinterval:\n4x / vierteljährlich";

            BindingControll(cbAdministrators, nameof(ManagementVM.Administrators));
            BindingControll(cbCustomers, nameof(ManagementVM.Customers));
            BindingControll(cbLocations, nameof(ManagementVM.Locations));
            BindingControll(cbMachineInformations, nameof(ManagementVM.MachineInformations));
            BindingControll(dgOthers, nameof(ManagementVM.OtherInformations));



            //cbAdministrators.SelectionEffectivelyChanged += CbAdministrators_SelectionEffectivelyChanged;
            //cbCustomers.SelectionEffectivelyChanged += CbCustomers_SelectionEffectivelyChanged;
            cbAdministrators.SelectionChanged += CbAdministrators_SelectionChanged;
            cbCustomers.SelectionChanged += CbCustomers_SelectionChanged;
            cbLocations.SelectionChanged += CbLocations_SelectionChanged;
            cbMachineInformations.SelectionChanged += CbMachineInformations_SelectionChanged;

            dgOthers.PreviewKeyDown += DgOthers_PreviewKeyDown;
            this.Loaded += ManagementView_Loaded;
            btnSearch.Click += BtnSearch_Click;
            txtSearch.KeyUp += TxtSearch_KeyUp;

            //TODO dgOthers cell  make vertical scrollabl
            //TODO Keyordnavigation


        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ShowOverviewFilterdItems();
                e.Handled = true;
            }

            if (e.Key == Key.Escape)
            {
                ShowOverviewFilterdItems(false);
                e.Handled = true;
            }
        }

        private void ShowOverviewFilterdItems(bool userFilter = true)
        {
            overviewFilterViewWindow.Show();
            string filter = txtSearch.Text;
            overviewFilterView.SetFilter(userFilter ? filter : "");
        }

        private void ManagementView_Loaded(object sender, RoutedEventArgs e)
        {
            InitOverviewFilterView();
            //overviewFilterView.SetDoubleClickEventHandler(Row_DoubleClick, Row_KeyUp);
            overviewFilterView.SetDoubleClickEventHandler(Row_DoubleClick);

            InitRecordView();

        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            ManagementOverviewFilter overviewFilter = row.DataContext as ManagementOverviewFilter; 
            SetMachineInfoOverviewFilter(overviewFilter);
            e.Handled = true;
            overviewFilterViewWindow.Hide();
        }
        //TODO not working
        //private void Row_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        var u = e.OriginalSource as UIElement;
        //        u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));

        //        DataGridRow row = sender as DataGridRow;
        //        ManagementOverviewFilter overviewFilter = row.DataContext as ManagementOverviewFilter;
        //        SetMachineInfoOverviewFilter(overviewFilter);
        //        e.Handled = true;
        //        overviewFilterViewWindow.Hide();
        //    }
        //}

        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
                ? Application.Current.Windows.OfType<T>().Any()
                : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            ShowOverviewFilterdItems();
            e.Handled = true;
        }

        private void InitOverviewFilterView()
        {
            overviewFilterView = new ManagementOverviewFilterView();
            overviewFilterViewWindow = new Window
            {
                Title = "Suche",
                Content = overviewFilterView,
                Name = "ManagementOverviewFilterWindow"
            };
            overviewFilterViewWindow.Height = 450;
            overviewFilterViewWindow.Width = 800;
            overviewFilterViewWindow.WindowStyle = WindowStyle.None;
            overviewFilterViewWindow.Topmost = true;

            var btnSearchLocation = txtSearch.PointToScreen(new Point(0, 0));

            //window.Width = btnSearch.ActualWidth + 16;
            //window.Height = 300;
            overviewFilterViewWindow.Left = btnSearchLocation.X + 8;
            overviewFilterViewWindow.Top = btnSearchLocation.Y + txtSearch.Height;

            overviewFilterView.btnCancel.Click += BtnCancel_Click;
            overviewFilterView.btnSave.Click += BtnSave_Click;
        }

        private void InitRecordView()
        {
            recordingView = new RecordingView();
            recordingViewWindow = new Window
            {
                Content = recordingView,
                Name = "ManagementRecordingViewWindow"
            };
            //recordingViewWindow.Height = 450;
            //recordingViewWindow.Width = 800;
           // recordingViewWindow.WindowStyle = WindowStyle.None;
            recordingViewWindow.Topmost = true;
            
           recordingView.btnCancel.Click += BtnCancelRecord_Click;
           recordingView.btnSave.Click += BtnSaveRecord_Click;

           recordingView.RcordingVM.Recordings= recordingsView.RcordingsVM.Recordings;

        }

        private void BtnSaveRecord_Click(object sender, RoutedEventArgs e)
        {
           recordingViewWindow.Hide();
        }

        private void BtnCancelRecord_Click(object sender, RoutedEventArgs e)
        {
            recordingViewWindow.Hide();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            overviewFilterViewWindow.Hide();

            var overviewFilter = overviewFilterView.dgOverviewFilter.SelectedItem as ManagementOverviewFilter;

            SetMachineInfoOverviewFilter(overviewFilter);
        }

        private void SetMachineInfoOverviewFilter(ManagementOverviewFilter overviewFilter)
        {
            var machineInformation = ManagementVM.MachineInformations.Where(c => c.Id == overviewFilter.MachineInformationId)
                .FirstOrDefault();
            SetFilterMachineInformation(machineInformation);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            overviewFilterViewWindow.Hide();
        }

        private void CbMachineInformations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFilterMachineInformation(cbMachineInformations.SelectedItem as MachineInformation);
        }

        private void SetFilterMachineInformation(MachineInformation machineInformation)
        {
            if (machineInformation == null)
                return;
            
            FilterMachineInformationSelected(machineInformation);

            var location = cbLocations.SelectedItem as Location;
            var customer = cbCustomers.SelectedItem as Customer;
            var machineInfo = cbMachineInformations.SelectedItem as MachineInformation;
            var maintenanceAgreement = ManagementVM.MaintenanceAgreements
                .Where(c => c.MachineInformationId == machineInfo.Id).FirstOrDefault();

            SetLocationData(location);
            SetCustomerData(customer);
            SetMaintenanceAgreementData(maintenanceAgreement);
            SetEmergencyAgreementData(maintenanceAgreement);
            SetMachineInformationData(machineInfo);

            List<ContactPartner> contactPersons = new List<ContactPartner>();

            contactPersons.AddRange(customer.Administrator.ContactPersons);
            contactPersons.Add(customer.ContactPerson);
            contactPersons.Add(location.ContactPerson);
            contactPersons.Add(machineInfo.ContactPerson);

            dgContactPersons.ItemsSource = contactPersons;
            dgContactPersons.Tag = ManagementVM.NotVisibleColumns;
        }

        private void CbLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterLocationSelected(cbLocations.SelectedItem as Location);

        }

        private void CbAdministrators_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterAdministratorSelected(cbAdministrators.SelectedItem as AdministratorCompany);
        }

        private void CbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterCustomerSelected(cbCustomers.SelectedItem as Customer);
        }

        private void CbCustomers_SelectionEffectivelyChanged(FilterableComboBox arg1, object arg2)
        {
            var customer = arg2 as Customer;
            cbLocations.ItemsSource = ManagementVM.Locations.Where(c => c.CustomerId == customer.Id);
            cbLocations.SelectedIndex = 0;
            var location = cbLocations.SelectedItem as Location;
            cbLocations.Text = location.ToString();

        }

        private void CbAdministrators_SelectionEffectivelyChanged(FilterableComboBox arg1, object arg2)
        {
            FilterAdministratorSelected(arg2 as AdministratorCompany);
        }

        private void FilterMachineInformationSelected(MachineInformation machineInformation)
        {
            if (machineInformation == null)
                return;

            var location = cbLocations.SelectedItem as Location;
            if (location == null || location.Id != machineInformation.LocationId)
            {
                SetFilterSelectedCbItem(cbLocations,
                    () => { return ManagementVM.Locations.Where(c => c.Id == machineInformation.LocationId); });
            }

        }

        private void FilterLocationSelected(Location location)
        {
            if (location == null)
                return;


            var customer = cbCustomers.SelectedItem as Customer;
            if (customer == null || customer.Id != location.CustomerId)
            {
                SetFilterSelectedCbItem(cbCustomers, () =>
                {
                    return ManagementVM.Customers.Where(c => c.Id == location.CustomerId);
                });
            }

            var machineInformation = cbMachineInformations.SelectedItem as MachineInformation;
            if (machineInformation == null || machineInformation.LocationId != location.Id)
            {
                SetFilterSelectedCbItem(cbMachineInformations, () =>
                {
                    return ManagementVM.MachineInformations.Where(c => c.LocationId == location.Id);
                });
            }
        }

        private void FilterCustomerSelected(Customer customer)
        {
            if (customer == null)
                return;

            var location = cbLocations.SelectedItem as Location;
            if (location == null || location.CustomerId != customer.Id)
            {
                SetFilterSelectedCbItem(cbLocations,
                    () => { return ManagementVM.Locations.Where(c => c.CustomerId == customer.Id); });
            }

            var administrators = cbAdministrators.SelectedItem as AdministratorCompany;
            if (administrators == null || administrators.CustomerId != customer.Id)
            {
                SetFilterSelectedCbItem(cbAdministrators,
                    () => { return ManagementVM.Administrators.Where(c => c.CustomerId == customer.Id); });
            }

        }

        private void FilterAdministratorSelected(AdministratorCompany administrator)
        {
            if (administrator == null)
                return;

            var customer = cbCustomers.SelectedItem as Customer;
            if (customer == null || customer.Id != administrator.CustomerId)
            {
                SetFilterSelectedCbItem(cbCustomers,
                    () => { return ManagementVM.Customers.Where(c => c.Id == administrator.CustomerId); });
            }
        }

        private void SetFilterSelectedCbItem(ComboBox comboBox, Func<IEnumerable> filteredSource)
        {
            if (filteredSource == null)
                return;

            comboBox.ItemsSource = filteredSource();
            comboBox.SelectedIndex = 0;
            var model = comboBox.SelectedItem as BaseDatabaseField;

            if (model != null)
                comboBox.Text = model.ToString();
        }

        private void BindingControll(ItemsControl control, string source)
        {
            Binding binding = new Binding("ManagementVM." + source)
            {
                Source = this
            };

            control.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        private void DgOthers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (dgOthers.SelectedIndex == dgOthers.Items.Count - 1)
                {
                    ManagementVM.OtherInformations.Add(new OtherInformation(""));
                    e.Handled = true;
                    //TODO dgOthers cell Textbox courser is set proper by new cell
                }

                dgOthers.CurrentCell = new DataGridCellInfo(
                    dgOthers.Items[dgOthers.SelectedIndex + 1], dgOthers.Columns[0]);
                dgOthers.BeginEdit();


            }
        }

        private void SetMaintenanceAgreementData(MaintenanceAgreement maintenanceAgreement)
        {
            lblMaintenanceDuration.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.Duration)) + ":";
            lblMaintenanceDate.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.AgreementDate)) + ":";
            lblMaintenanceNoticeOfPeriod.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.NoticeOfPeriod)) + ":";
            lblMaintenanceCanBeCancelled.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.CanBeCancelled)) + ":";

            lblMaintenanceDurationValue.Content = maintenanceAgreement.Duration.ToString("dd.MM.yyyy");
            lblMaintenanceDateValue.Content = maintenanceAgreement.AgreementDate.ToString("dd.MM.yyyy");
            lblMaintenanceNoticeOfPeriodValue.Content = maintenanceAgreement.NoticeOfPeriod;
            lblMaintenanceCanBeCancelledValue.Content = maintenanceAgreement.CanBeCancelled;
        }

        //TODO
        private void SetEmergencyAgreementData(MaintenanceAgreement maintenanceAgreement)
        {
            lblEmergencyDuration.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.Duration)) + ":";
            lblEmergencyDate.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.AgreementDate)) + ":";
            lblEmergencyNoticeOfPeriod.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.NoticeOfPeriod)) + ":";
            lblEmergencyCanBeCancelled.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.CanBeCancelled)) + ":";

            lblEmergencyDurationValue.Content = maintenanceAgreement.Duration.ToString("dd.MM.yyyy");
            lblEmergencyDateValue.Content = maintenanceAgreement.AgreementDate.ToString("dd.MM.yyyy");
            lblEmergencyNoticeOfPeriodValue.Content = maintenanceAgreement.NoticeOfPeriod;
            lblEmergencyCanBeCancelledValue.Content = maintenanceAgreement.CanBeCancelled;
        }

        private void SetMachineInformationData(MachineInformation machineInformation)
        {
            lblMachineInfoName.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Name)) + ":";
            lblMachineInfoSerialNumber.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.SerialNumber)) + ":";
            lblMachineInfoHoldingPositions.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.HoldingPositions)) + ":";
            lblMachineInfoEntrances.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Entrances)) + ":";
            lblMachineInfoYearOfConstruction.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.YearOfConstruction)) + ":";
            lblMachineInfoPayload.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Payload)) + ":";

            lblMachineInfoNameValue.Content = machineInformation.Name;
            lblMachineInfoSerialNumberValue.Content = machineInformation.SerialNumber;
            lblMachineInfoHoldingPositionsValue.Content = machineInformation.HoldingPositions;
            lblMachineInfoEntrancesValue.Content = machineInformation.Entrances;
            lblMachineInfoYearOfConstructionValue.Content = machineInformation.YearOfConstruction.ToString("dd.MM.yyyy");
            lblMachineInfoPayloadValue.Content = machineInformation.Payload;
        }

        private void SetLocationData(Location location)
        {
            lblAddressLocation.Content = location.Address;
            lblPostcodeCityLocation.Content = location.GetPostcodeCity();
        }
        private void SetCustomerData(Customer customer)
        {
            lblCompanyName.Content = customer.CompanyName;
            lblAdministratorCompany.Content = customer.Administrator.Name;
            lblAddress.Content = customer.Address;
            lblPostcodeCity.Content = customer.GetPostcodeCity();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var vm= new RecordingViewModel();
            vm.Record = recordingsView.GetSelectedItem();
            vm.Recordings = recordingsView.RcordingsVM.Recordings;

            recordingView.RcordingVM = vm;
            recordingView.SetData();
            recordingViewWindow.ShowDialog();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var vm = new RecordingViewModel();
            vm.Recordings = recordingsView.RcordingsVM.Recordings;
            recordingView.RcordingVM = vm;

            recordingViewWindow.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //TODO PW
        }
    }

}
