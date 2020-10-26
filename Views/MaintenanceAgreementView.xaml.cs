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
using Liftmanagement.Common;
using Liftmanagement.Converters;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for MaintenanceAgreementView.xaml
    /// </summary>
    public partial class MaintenanceAgreementView : GoogleDriveDialogueView
    {
        private CustomersView customersView ;
        private LocationsView locationsView ;
        private MachineInformationsView machineInfosView;
        private MasterDataInfoView masterDataInfo;

        public MaintenanceAgreementViewModel MaintenanceAgreementVM { get; set; } = new MaintenanceAgreementViewModel();

        public MaintenanceAgreementView()
        {
            InitializeComponent();
            AssignSelectAllForTextBoxes();

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

            //customersView.expanderCustomers.Collapsed += ExpanderCustomers_Collapsed;
            customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;

            //locationsView.expanderLocations.Collapsed += ExpanderLocations_Collapsed;
            locationsView.dgLocations.SelectionChanged += DgLocations_SelectionChanged;

           // machineInformationsView.expanderMachineInformations.Collapsed += ExpanderMachineInformations_Collapsed;
            machineInformationsView.dgMachineInformations.SelectionChanged += DgMachineInformations_SelectionChanged;

            NotVisibleColumns.Add(nameof(MaintenanceAgreement.GoogleCalendarEventId));
            dgMaintenanceAgreements.Tag = NotVisibleColumns;

            dgMaintenanceAgreements.SelectionChanged += DgMaintenanceAgreements_SelectionChanged;
            dgMaintenanceAgreements.SelectedIndex = 0;

            MaintenanceAgreementVM.LoadComboboxes();
            AssigneValuesToControl();

            cbNoticeOfPeriodMonth.ItemsSource = GetMonths();
            cbNoticeOfPeriodMonth.SelectedIndex = 2;

            txtNoticeOfPeriod.Text = "Monate";

            EnableContoles(false);

        }

        private void CbMachineInformations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var machineInformation = GetSelectedObject<MachineInformation>(sender);

            if (machineInformation != null)
                MaintenanceAgreementVM.RefreshByMachineInformatio(machineInformation.Id);
        }


        private void ExpanderMachineInformations_Collapsed(object sender, RoutedEventArgs e)
        {
            gridResizableMaintenanceAgreements.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Auto);
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

        private void AssigneValuesToControl()
        {
            lblDuration.Content = MaintenanceAgreementVM.MaintenanceAgreementSelected.GetDisplayName<MaintenanceAgreement>(nameof(MaintenanceAgreementVM.MaintenanceAgreementSelected.Duration)) + ":";
            lblTerminated.Content =
                MaintenanceAgreementVM.MaintenanceAgreementSelected.GetDisplayName<MaintenanceAgreement>(nameof(MaintenanceAgreementVM.MaintenanceAgreementSelected.CanBeCancelled)) + ":";
            lblNoticeOfPeriod.Content =
                MaintenanceAgreementVM.MaintenanceAgreementSelected.GetDisplayName<MaintenanceAgreement>(nameof(MaintenanceAgreementVM.MaintenanceAgreementSelected.NoticeOfPeriod)) + ":";
            lblAgreementDate.Content =
                MaintenanceAgreementVM.MaintenanceAgreementSelected.GetDisplayName<MaintenanceAgreement>(nameof(MaintenanceAgreementVM.MaintenanceAgreementSelected.AgreementDate)) + ":";
            lblMaintenanceType.Content = MaintenanceAgreementVM.MaintenanceAgreementSelected.GetDisplayName<MaintenanceAgreement>(nameof(MaintenanceAgreementVM.MaintenanceAgreementSelected.MaintenanceType)) +
                                         ":";
            lblAdditionalInfo.Content =
                MaintenanceAgreementVM.MaintenanceAgreementSelected.GetDisplayName<MaintenanceAgreement>(nameof(MaintenanceAgreementVM.MaintenanceAgreementSelected.AdditionalInfo)) + ":";
            lblNotificationTime.Content =
                MaintenanceAgreementVM.MaintenanceAgreementSelected.GetDisplayName<MaintenanceAgreement>(nameof(MaintenanceAgreementVM.MaintenanceAgreementSelected.NotificationTime)) + ":";
            lblArreementCancelledBy.Content =
                MaintenanceAgreementVM.MaintenanceAgreementSelected.GetDisplayName<MaintenanceAgreement>(nameof(MaintenanceAgreementVM.MaintenanceAgreementSelected.AgreementCancelledBy)) + ":";
            lblGoogleDriveLink.Content =
                MaintenanceAgreementVM.MaintenanceAgreementSelected.GetDisplayName<MaintenanceAgreement>(nameof(MaintenanceAgreementVM.MaintenanceAgreementSelected.GoogleDriveFolderName)) + ":";
            lblGoogleCalendarLink.Content =
                MaintenanceAgreementVM.MaintenanceAgreementSelected.GetDisplayName<MaintenanceAgreement>(nameof(MaintenanceAgreementVM.MaintenanceAgreementSelected.GoogleCalendarEventId)) + ":";


            BindingText(txtAdditionalInfo, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.AdditionalInfo);

            //BindingDatePicker(datePickerDuration, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.Duration,true,()=>new MandatoryRule());
            //
            //
            BindingDatePicker(datePickerDuration, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.Duration);
            //datePickerDuration.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Now.AddDays(-2)));

            //BindingDatePicker(datePickerAgreementDate, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.AgreementDate, true, () => new DatetimeRule());
            //datePickerAgreementDate.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, Helper.Helper.DefaultDate));

          // BindingDatetime(txtDuration, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.Duration);
            BindingDatetime(txtAgreementDate, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.AgreementDate);

            BindingText(txtNotificationTime, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.NotificationTime);

            BindingText(txtNotificationTime, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.NotificationTime);

            BindingComboBoxBindingModeOneWay(cbNotificationUnit,()=>MaintenanceAgreementVM.NotificationUnits);
           BindingComboBoxSelectedValue(cbNotificationUnit,()=> MaintenanceAgreementVM.MaintenanceAgreementSelected.NotificationUnit);
       
           BindingComboBoxBindingModeOneWay(cbArreementCancelledBy, () => MaintenanceAgreementVM.ArreementCancelledBy);
           BindingComboBoxText(cbArreementCancelledBy, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.AgreementCancelledBy);

            BindingComboBoxBindingModeOneWay(cbMaintenanceType, () => MaintenanceAgreementVM.MaintenanceTypes);
            BindingComboBoxText(cbMaintenanceType, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.MaintenanceType);

            BindingComboBoxBindingModeOneWay(cbTerminated,()=>MaintenanceAgreementVM.TerminationUnits);
            BindingComboBoxText(cbTerminated, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.CanBeCancelled);

            BindingHyperlink(hyperlinkGoogleDrive, ()=> MaintenanceAgreementVM.MaintenanceAgreementSelected.GoogleDriveLink);
            BindingTextBlock(txtGoogleDriveFolderName, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.GoogleDriveFolderName);

            BindingHyperlink(hyperlinkGoogleCalendar, ()=> MaintenanceAgreementVM.MaintenanceAgreementSelected.GoogleCalendarEventId);
            BindingTextBlockVisibility(txtGoogleCalendarHyperlink, () => MaintenanceAgreementVM.MaintenanceAgreementSelected.GoogleCalendarEventId, ()=>new ValueVisibilityConverter());

            BindingControl(dgMaintenanceAgreements,()=>MaintenanceAgreementVM.MaintenanceAgreements);
        }
        private void CbLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        //    var location = GetSelectedObject<Location>(sender);

        //    if (location != null)
        //        MaintenanceAgreementVM.RefreshByLocation(location.Id);
        }

        protected override void EnableContoles(bool enable)
        {
            base.EnableContoles(enable);
            txtNoticeOfPeriod.IsEnabled = false;
        }

        private void DgMaintenanceAgreements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MaintenanceAgreementVM.MaintenanceAgreementSelected = GetSelectedObject<MaintenanceAgreement>(sender);
            EnableContoles(false);
            // cbTerminated.SelectedIndex = 0;
            //cbNoticeOfPeriodMonth.SelectedIndex = 2;
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

        protected override void BtnSaveGoogleDrive_Click(object sender, RoutedEventArgs e)
        {
            var node = googlDriveTree.GetSelectedNode();

            MaintenanceAgreementVM.MaintenanceAgreementSelected.GoogleDriveLink = node.WebLink;
            MaintenanceAgreementVM.MaintenanceAgreementSelected.GoogleDriveFolderName = node.Name;

            windowGoogleDriveTree.Hide();
        }

        private void btnGoogleDriveTree_Click(object sender, RoutedEventArgs e)
        {
            if (MaintenanceAgreementVM.MaintenanceAgreementSelected != null)
            {
                windowGoogleDriveTree.Title = windowGoogleDriveTree.Title + "  " + MaintenanceAgreementVM.MaintenanceAgreementSelected.GetFullName();
            }

            windowGoogleDriveTree.ShowDialog();
        }

        private void ForceValidation()
        {
            //txtName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            //txtYearOfConstruction.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            //txtSerialNumber.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ForceValidation();
                bool isValid = true;
                List<string> validationMsg = new List<string>();


                if (MaintenanceAgreementVM.MaintenanceAgreementSelected.Duration<= Helper.Helper.DefaultDate)
                {
                    validationMsg.Add("Vertragslaufzeitdatum ist unzulässig.");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(MaintenanceAgreementVM.MaintenanceAgreementSelected.CanBeCancelled))
                {
                    validationMsg.Add("Kündigungsmodus darf nicht leer sein.");
                    isValid = false;
                }
                if (MaintenanceAgreementVM.MaintenanceAgreementSelected.AgreementDate <= Helper.Helper.DefaultDate)
                {
                    validationMsg.Add("Vertragsdatum ist unzulässig.");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(MaintenanceAgreementVM.MaintenanceAgreementSelected.CanBeCancelled))
                {
                    validationMsg.Add("Wartungsart darf nicht leer sein.");
                    isValid = false;
                }

                if (!isValid)
                {
                    new NotificationWindow("Fehler!", null,validationMsg,NotificationWindow.MessageType.Error).Show();
                    return;
                }

                var result = MaintenanceAgreementVM.Add(masterDataInfo.MasterDataInfoVM);
                if (result.Records > 0)
                {
                    MaintenanceAgreementVM.RefreshByMachineInformatio(masterDataInfo.MasterDataInfoVM.MachineInformationSelected.Id);
                    var maintenanceAgreement = dgMaintenanceAgreements.Items.Cast<MaintenanceAgreement>().Single(c => c.Id == result.Id);
                    dgMaintenanceAgreements.SelectedItem = maintenanceAgreement;

                    var titel = string.Format("Anlage : {0}", maintenanceAgreement.GetFullName());
                    var msg = "Anlagedaten wurden gespeichert.";
                    new NotificationWindow(titel, msg).Show();
                }
                else
                {
                    var msg = "Anlagedaten konnten nicht gespeichert werden.";
                    new NotificationWindow("Fehler!", msg).Show();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                var msg = "Anlagedaten konnten nicht gespeichert werden.";
                new NotificationWindow("Fehler!", exception.ToString(),null,NotificationWindow.MessageType.Error).Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceAgreementVM.MarkForDeleteMaintenanceAgreement();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceAgreementVM.MaintenanceAgreementSelectedLast = MaintenanceAgreementVM.MaintenanceAgreementSelected;
            MaintenanceAgreementVM.MaintenanceAgreementSelected = new MaintenanceAgreement();
            cbNotificationUnit.SelectedIndex = 2;
            cbArreementCancelledBy.SelectedIndex = 0;
            cbMaintenanceType.SelectedIndex = 0;
            cbTerminated.SelectedIndex = 0;

            EnableContoles(true);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (MaintenanceAgreementVM.MaintenanceAgreementSelected == null ||
                MaintenanceAgreementVM.MaintenanceAgreements.Count == 0)
            {
                new NotificationWindow("Fehler!", "Es sind keine Wartungsverträge ausgewählt um zu bearbeiten",null, NotificationWindow.MessageType.Waring).Show();
                return;
            }

            MaintenanceAgreementVM.MaintenanceAgreementSelectedLast = MaintenanceAgreementVM.MaintenanceAgreementSelected;
            var result = MaintenanceAgreementVM.EditMaintenanceAgreement();
            if (result.IsReadOnly)
            {
                AskForceToEdit(result.CurrentlyUsedBy, () =>
                {
                    MaintenanceAgreementVM.ForceEditing();
                    EnableContoles(true);
                });
            }
            else
            {
                EnableContoles(true);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MaintenanceAgreementVM.MaintenanceAgreementSelected.ReadOnly)
            {
                MaintenanceAgreementVM.ReleaseEditing();
            }

            MaintenanceAgreementVM.MaintenanceAgreementSelected = (MaintenanceAgreement) dgMaintenanceAgreements.SelectedItem;
            EnableContoles(false);

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

       

        //private void btnSave_Click(object sender, RoutedEventArgs e)
        //{
        //    var date = datePickerDuration.SelectedDate ?? DateTime.Now;
        //    int months = (int)cbNoticeOfPeriodMonth.SelectedItem;
        //    string summary = string.Empty;
        //    string description = string.Empty;

        //    var NoticeOfPeriodDate = date.AddMonths((months * (-1)));

        //    //var customer= customersView.dgCustomers.SelectedItem as Customer;
        //    //if (customer != null)
        //    //{
        //    //    summary = customer.CompanyName + ", " + customer.City;
        //    //}

        //    //var machineInformation = machineInfosView.dgMachineInformations.SelectedItem as MachineInformation;
        //    //if (machineInformation != null)
        //    //{
        //    //    description = machineInformation.Name + ", " + machineInformation.SerialNumber;
        //    //}

        //    summary = masterDataInfo.lblCompanyName.Content.ToString() + ", " + masterDataInfo.lblPostcodeCity.Content.ToString();
        //    description = masterDataInfo.lblMachineName.Content.ToString() + ", " + masterDataInfo.lblSerialNumber.Content.ToString();


        //    new CalendarQuickstart(NoticeOfPeriodDate, NoticeOfPeriodDate.AddMinutes(30), summary, description);


        //}

       
    }
}
