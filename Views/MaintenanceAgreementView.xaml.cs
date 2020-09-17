using Liftmanagement.Helper;
using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for MaintenanceAgreementView.xaml
    /// </summary>
    public partial class MaintenanceAgreementView : UserControl
    {
        private CustomersView customersView;
        private MachineInformationsView machineInfosView;
        private MasterDataInfoView masterDataInfo;

        public MaintenanceAgreementView()
        {
            InitializeComponent();

            customersView = new CustomersView();
            //customersView.spCustomers.MinWidth = 50;
            frameCustomers.Content = customersView;

           var locationsView = new LocationsView();
            //locationsView.spLocations.MinWidth = 50;
            frameLocations.Content = locationsView;

            machineInfosView = new MachineInformationsView();
            machineInfosView.NotVisibleColumns.AddRange ( MachineInformation.GetPropertiesStringNames<MachineInformation>(new List<string> { "Name", "SerialNumber", "YearOfConstruction" }));
            frameMachineInformations.Content = machineInfosView;

            var maintenanceAgreementsView = new MaintenanceAgreementsView();
            frameMaintenanceAgreements.Content = maintenanceAgreementsView;
            maintenanceAgreementsView.dgMaintenanceAgreements.SelectionChanged += DgMaintenanceAgreements_SelectionChanged;

            masterDataInfo = new MasterDataInfoView();
            frameMasterDataInfo.Content = masterDataInfo;



            cbTerminated.ItemsSource = Enum.GetValues(typeof(Helper.Helper.TTypeTermination)).Cast<Helper.Helper.TTypeTermination>();
            cbTerminated.SelectedIndex = 0;

            cbNoticeOfPeriodMonth.ItemsSource = GetMonths();
            cbNoticeOfPeriodMonth.SelectedIndex = 2;

            txtNoticeOfPeriod.Text = "Monate";

            customersView.expanderCustomers.Collapsed += ExpanderCustomers_Collapsed;
            customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;
            customersView.spCustomers.Loaded += Stackpanel_Loaded;

            locationsView.expanderLocations.Collapsed += ExpanderLocations_Collapsed;
            locationsView.dgLocations.SelectionChanged += DgLocations_SelectionChanged;
            locationsView.spLocations.Loaded += Stackpanel_Loaded;

            expanderMachineInformations.Collapsed += ExpanderMachineInformations_Collapsed;
            machineInfosView.dgMachineInformations.SelectionChanged += DgMachineInformations_SelectionChanged;
            machineInfosView.spMachineInformations.Loaded += Stackpanel_Loaded;
            txtExpanderHeader.Text = new CategoryViewModel().Categories.Where(c => c.MangementType == Helper.Helper.TTypeMangement.MachineInformation).Select(c => c.Name).FirstOrDefault();

            maintenanceAgreementsView.dgMaintenanceAgreements.SelectedIndex = 0;
            
        }

        private IEnumerable GetMonths()
        {
            List<int> months = new List<int>();
            for (int i = 1; i < 13; i++)
            {
                months.Add(i);
            }
            return months;
        }

        private void DgMaintenanceAgreements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MaintenanceAgreement maintenanceAgreement = null;
            var dgMaintenanceAgreements = sender as DataGrid;
            if (dgMaintenanceAgreements != null)
                maintenanceAgreement = dgMaintenanceAgreements.SelectedItem as MaintenanceAgreement;

            if (maintenanceAgreement == null)
            {
                return;
            }

            lblAdditionalInfo.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.AdditionalInfo)) + ":";
            lblDuration.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.Duration)) + ":";
            lblTerminated.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.CanBeCancelled )) + ":";
            lblNoticeOfPeriod.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.NoticeOfPeriod)) + ":";
            lblAgreementDate.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.AgreementDate)) + ":";         

            txtAdditionalInfo.Text = maintenanceAgreement.AdditionalInfo;
            //datePickerDuration.SelectedDate = DateTime.ParseExact(maintenanceAgreement.Duration, "dd.MM.yyyy",
            //                            System.Globalization.CultureInfo.InvariantCulture); 
            // datePickerAgreementDate.SelectedDate= DateTime.ParseExact(maintenanceAgreement.AgreementDate, "dd.MM.yyyy",
            //                            System.Globalization.CultureInfo.InvariantCulture);
            datePickerDuration.SelectedDate = maintenanceAgreement.Duration;
            datePickerAgreementDate.SelectedDate = maintenanceAgreement.AgreementDate;

            cbTerminated.SelectedIndex = 0;
            cbNoticeOfPeriodMonth.SelectedIndex = 2;
            
           
            //txtNoticeOfPeriod.Text = maintenanceAgreement.NoticeOfPeriod;
            //txtAgreementDate.Text = maintenanceAgreement.AgreementDate;
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

        private void DgMachineInformations_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void ExpanderMachineInformations_Collapsed(object sender, RoutedEventArgs e)
        {
            gridResizableLocations.ColumnDefinitions[4].Width = new GridLength(1, GridUnitType.Auto);
        }

        private void Stackpanel_Loaded(object sender, RoutedEventArgs e)
        {
            var stackpanel = sender as StackPanel;
            if (stackpanel == null)
                return;

            //Work around           
            stackpanel.Width = stackpanel.Width+1;
           // stackpanel.Width = 450;
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
            gridResizableLocations.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }

        private void ColumnDefinition_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void datePickerDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
           // Regex regex = new Regex("[^0-9/]+");
            Regex regex = new Regex(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");
            return !regex.IsMatch(text);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnSave.Visibility = Visibility.Visible;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var date= datePickerDuration.SelectedDate??DateTime.Now;
            int months =(int) cbNoticeOfPeriodMonth.SelectedItem;
            string summary = string.Empty;
            string description = string.Empty;

          var NoticeOfPeriodDate =date.AddMonths((months *(- 1)));

            //var customer= customersView.dgCustomers.SelectedItem as Customer;
            //if (customer != null)
            //{
            //    summary = customer.CompanyName + ", " + customer.City;
            //}

            //var machineInformation = machineInfosView.dgMachineInformations.SelectedItem as MachineInformation;
            //if (machineInformation != null)
            //{
            //    description = machineInformation.Name + ", " + machineInformation.SerialNumber;
            //}

            summary = masterDataInfo.lblCompanyName.Content.ToString() + ", " + masterDataInfo.lblPostcodeCity.Content.ToString();
            description = masterDataInfo.lblMachineName.Content.ToString() + ", " + masterDataInfo.lblSerialNumber.Content.ToString();


            new CalendarQuickstart(NoticeOfPeriodDate, NoticeOfPeriodDate.AddMinutes(30), summary, description);

           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
