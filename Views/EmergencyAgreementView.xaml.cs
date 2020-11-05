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
using System.Windows.Threading;
using Liftmanagement.Common;
using Liftmanagement.Converters;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for EmergencyAgreementView.xaml
    /// </summary>
    public partial class EmergencyAgreementView : GoogleDriveDialogueView
    {
        private CustomersView customersView;
        private LocationsView locationsView;
        private MachineInformationsView machineInfosView;
        private MasterDataInfoView masterDataInfo;
        private Window emergencyAgreementContentViewWindow;

        public EmergencyAgreementViewModel EmergencyAgreementVM { get; set; } = new EmergencyAgreementViewModel();

        public EmergencyAgreementView()
        {
            InitializeComponent();
            AssignSelectAllForTextBoxes();

            LoadingIndicatorPanel.Visibility = Visibility.Visible;
            gridResizableEmergencyAgreements.IsEnabled = false;

            Task.Factory.StartNew(() =>
            {
                var customersVM = new CustomersViewModel();
                customersVM.RefreshOnlyCustomers();

              var result=  Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() =>
                    {
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

                        masterDataInfo = new MasterDataInfoView(Helper.Helper.TTypeMangement.EmergencyAgreement);
                        masterDataInfo.cbMachineInformations.SelectionChanged += CbMachineInformations_SelectionChanged;

                        masterDataInfo.RefreshCustomers();
                        frameMasterDataInfo.Content = masterDataInfo;

                        customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;

                        locationsView.dgLocations.SelectionChanged += DgLocations_SelectionChanged;

                        machineInformationsView.dgMachineInformations.SelectionChanged += DgMachineInformations_SelectionChanged;

                        NotVisibleColumns.Add(nameof(EmergencyAgreement.GoogleCalendarEventId));
                        dgEmergencyAgreements.Tag = NotVisibleColumns;

                        dgEmergencyAgreements.SelectionChanged += DgEmergencyAgreements_SelectionChanged;
                        dgEmergencyAgreements.SelectedIndex = 0;

                        EmergencyAgreementVM.LoadComboboxes();
                        AssigneValuesToControl();

                        cbNoticeOfPeriodMonth.ItemsSource = GetMonths();
                        cbNoticeOfPeriodMonth.SelectedIndex = 2;

                        txtNoticeOfPeriod.Text = "Monate";

                        EnableContoles(false);
                    }));

            }).ContinueWith((task) =>
            {
                LoadingIndicatorPanel.Visibility = Visibility.Collapsed;
                gridResizableEmergencyAgreements.IsEnabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
            
        }

        private void CbMachineInformations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var machineInformation = GetSelectedObject<MachineInformation>(sender);

            if (machineInformation != null)
                EmergencyAgreementVM.RefreshByMachineInformatio(machineInformation.Id);
        }


        private void ExpanderMachineInformations_Collapsed(object sender, RoutedEventArgs e)
        {
            gridResizableEmergencyAgreements.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Auto);
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
            lblDuration.Content = EmergencyAgreementVM.EmergencyAgreementSelected.GetDisplayName<EmergencyAgreement>(nameof(EmergencyAgreementVM.EmergencyAgreementSelected.Duration)) + ":";
            lblTerminated.Content =
                EmergencyAgreementVM.EmergencyAgreementSelected.GetDisplayName<EmergencyAgreement>(nameof(EmergencyAgreementVM.EmergencyAgreementSelected.CanBeCancelled)) + ":";
            lblNoticeOfPeriod.Content =
                EmergencyAgreementVM.EmergencyAgreementSelected.GetDisplayName<EmergencyAgreement>(nameof(EmergencyAgreementVM.EmergencyAgreementSelected.NoticeOfPeriod)) + ":";
            lblAgreementDate.Content =
                EmergencyAgreementVM.EmergencyAgreementSelected.GetDisplayName<EmergencyAgreement>(nameof(EmergencyAgreementVM.EmergencyAgreementSelected.AgreementDate)) + ":";
            lblMaintenanceType.Content = EmergencyAgreementVM.EmergencyAgreementSelected.GetDisplayName<EmergencyAgreement>(nameof(EmergencyAgreementVM.EmergencyAgreementSelected.EmergencyType)) +
                                         ":";
            lblAdditionalInfo.Content =
                EmergencyAgreementVM.EmergencyAgreementSelected.GetDisplayName<EmergencyAgreement>(nameof(EmergencyAgreementVM.EmergencyAgreementSelected.AdditionalInfo)) + ":";
            lblNotificationTime.Content =
                EmergencyAgreementVM.EmergencyAgreementSelected.GetDisplayName<EmergencyAgreement>(nameof(EmergencyAgreementVM.EmergencyAgreementSelected.NotificationTime)) + ":";
            lblArreementCancelledBy.Content =
                EmergencyAgreementVM.EmergencyAgreementSelected.GetDisplayName<EmergencyAgreement>(nameof(EmergencyAgreementVM.EmergencyAgreementSelected.AgreementCancelledBy)) + ":";
            lblGoogleDriveLink.Content =
                EmergencyAgreementVM.EmergencyAgreementSelected.GetDisplayName<EmergencyAgreement>(nameof(EmergencyAgreementVM.EmergencyAgreementSelected.GoogleDriveFolderName)) + ":";
            lblGoogleCalendarLink.Content =
                EmergencyAgreementVM.EmergencyAgreementSelected.GetDisplayName<EmergencyAgreement>(nameof(EmergencyAgreementVM.EmergencyAgreementSelected.GoogleCalendarEventId)) + ":";


            BindingText(txtAdditionalInfo, () => EmergencyAgreementVM.EmergencyAgreementSelected.AdditionalInfo);

            //BindingDatePicker(datePickerDuration, () => EmergencyAgreementVM.EmergencyAgreementSelected.Duration,true,()=>new MandatoryRule());
            //
            //
            BindingDatePicker(datePickerDuration, () => EmergencyAgreementVM.EmergencyAgreementSelected.Duration);
            //datePickerDuration.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Now.AddDays(-2)));

            //BindingDatePicker(datePickerAgreementDate, () => EmergencyAgreementVM.EmergencyAgreementSelected.AgreementDate, true, () => new DatetimeRule());
            //datePickerAgreementDate.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, Helper.Helper.DefaultDate));

            // BindingDatetime(txtDuration, () => EmergencyAgreementVM.EmergencyAgreementSelected.Duration);
            BindingDatetime(txtAgreementDate, () => EmergencyAgreementVM.EmergencyAgreementSelected.AgreementDate);

            BindingText(txtNotificationTime, () => EmergencyAgreementVM.EmergencyAgreementSelected.NotificationTime);

            BindingText(txtNotificationTime, () => EmergencyAgreementVM.EmergencyAgreementSelected.NotificationTime);

            BindingComboBoxBindingModeOneWay(cbNotificationUnit, () => EmergencyAgreementVM.NotificationUnits);
            BindingComboBoxSelectedValue(cbNotificationUnit, () => EmergencyAgreementVM.EmergencyAgreementSelected.NotificationUnit);

            BindingComboBoxBindingModeOneWay(cbArreementCancelledBy, () => EmergencyAgreementVM.ArreementCancelledBy);
            BindingComboBoxText(cbArreementCancelledBy, () => EmergencyAgreementVM.EmergencyAgreementSelected.AgreementCancelledBy);

            BindingComboBoxBindingModeOneWay(cbEmergencyType, () => EmergencyAgreementVM.EmergencyTypes);
            BindingComboBoxText(cbEmergencyType, () => EmergencyAgreementVM.EmergencyAgreementSelected.EmergencyType);

            BindingComboBoxBindingModeOneWay(cbTerminated, () => EmergencyAgreementVM.TerminationUnits);
            BindingComboBoxText(cbTerminated, () => EmergencyAgreementVM.EmergencyAgreementSelected.CanBeCancelled);

            BindingHyperlink(hyperlinkGoogleDrive, () => EmergencyAgreementVM.EmergencyAgreementSelected.GoogleDriveLink);
            BindingTextBlock(txtGoogleDriveFolderName, () => EmergencyAgreementVM.EmergencyAgreementSelected.GoogleDriveFolderName);

            BindingHyperlink(hyperlinkGoogleCalendar, () => EmergencyAgreementVM.EmergencyAgreementSelected.GoogleCalendarEventId);
            BindingTextBlockVisibility(txtGoogleCalendarHyperlink, () => EmergencyAgreementVM.EmergencyAgreementSelected.GoogleCalendarEventId, () => new ValueVisibilityConverter());

            BindingControl(dgEmergencyAgreements, () => EmergencyAgreementVM.EmergencyAgreements);
        }
        private void CbLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //    var location = GetSelectedObject<Location>(sender);

            //    if (location != null)
            //        EmergencyAgreementVM.RefreshByLocation(location.Id);
        }

        protected override void EnableContoles(bool enable)
        {
            base.EnableContoles(enable);
            txtNoticeOfPeriod.IsEnabled = false;
        }

        private void DgEmergencyAgreements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EmergencyAgreementVM.EmergencyAgreementSelected = GetSelectedObject<EmergencyAgreement>(sender);
            EnableContoles(false);
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
            gridResizableEmergencyAgreements.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);
        }

        private void DgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = GetSelectedObject<Customer>(sender);

            if (customer != null)
                masterDataInfo.cbCustomers.SelectedItem = masterDataInfo.cbCustomers.Items.Cast<Customer>().Single(c => c.Id == customer.Id);

        }
        private void ExpanderCustomers_Collapsed(object sender, RoutedEventArgs e)
        {
            gridResizableEmergencyAgreements.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }

        protected override void BtnSaveGoogleDrive_Click(object sender, RoutedEventArgs e)
        {
            var node = googlDriveTree.GetSelectedNode();

            EmergencyAgreementVM.EmergencyAgreementSelected.GoogleDriveLink = node.WebLink;
            EmergencyAgreementVM.EmergencyAgreementSelected.GoogleDriveFolderName = node.Name;

            windowGoogleDriveTree.Hide();
        }

        private void btnGoogleDriveTree_Click(object sender, RoutedEventArgs e)
        {
            if (EmergencyAgreementVM.EmergencyAgreementSelected != null)
            {
                windowGoogleDriveTree.Title = windowGoogleDriveTree.Title + "  " + EmergencyAgreementVM.EmergencyAgreementSelected.GetFullName();
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


                if (EmergencyAgreementVM.EmergencyAgreementSelected.Duration <= Helper.Helper.DefaultDate)
                {
                    validationMsg.Add("Vertragslaufzeitdatum ist unzulässig.");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(EmergencyAgreementVM.EmergencyAgreementSelected.CanBeCancelled))
                {
                    validationMsg.Add("Kündigungsmodus darf nicht leer sein.");
                    isValid = false;
                }
                if (EmergencyAgreementVM.EmergencyAgreementSelected.AgreementDate <= Helper.Helper.DefaultDate)
                {
                    validationMsg.Add("Vertragsdatum ist unzulässig.");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(EmergencyAgreementVM.EmergencyAgreementSelected.CanBeCancelled))
                {
                    validationMsg.Add("Wartungsart darf nicht leer sein.");
                    isValid = false;
                }

                if (!isValid)
                {
                    new NotificationWindow("Fehler!", null, validationMsg, NotificationWindow.MessageType.Error).Show();
                    return;
                }

                var result = EmergencyAgreementVM.Add(masterDataInfo.MasterDataInfoVM);
                if (result.Records > 0)
                {
                    EmergencyAgreementVM.RefreshByMachineInformatio(masterDataInfo.MasterDataInfoVM.MachineInformationSelected.Id);
                    var emergencyAgreement = dgEmergencyAgreements.Items.Cast<EmergencyAgreement>().Single(c => c.Id == result.Id);
                    dgEmergencyAgreements.SelectedItem = emergencyAgreement;

                    var titel = string.Format("Notrufvertrag : {0}", emergencyAgreement.GetFullName());
                    var msg = "Notrufvertrag wurde gespeichert.";
                    new NotificationWindow(titel, msg).Show();
                }
                else
                {
                    var msg = "Notrufvertrag konnte nicht gespeichert werden.";
                    new NotificationWindow("Fehler!", msg).Show();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                var msg = "Notrufvertrag konnte nicht gespeichert werden.";
                new NotificationWindow("Fehler!", exception.ToString(), null, NotificationWindow.MessageType.Error).Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MarkForDelete("Notrufvertrag", EmergencyAgreementVM.EmergencyAgreementSelected,
                () => EmergencyAgreementVM.MarkForDeleteEmergencyAgreement(),
                () => {
                                    EmergencyAgreementVM.RefreshByMachineInformatio(masterDataInfo.MasterDataInfoVM.MachineInformationSelected.Id);
                                    dgEmergencyAgreements.SelectedIndex = 0;
                                    });

          
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmergencyAgreementVM.EmergencyAgreementSelectedLast = EmergencyAgreementVM.EmergencyAgreementSelected;
            EmergencyAgreementVM.EmergencyAgreementSelected = new EmergencyAgreement();
            cbNotificationUnit.SelectedIndex = 2;
            cbArreementCancelledBy.SelectedIndex = 0;
            cbEmergencyType.SelectedIndex = 0;
            cbTerminated.SelectedIndex = 0;

            EnableContoles(true);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (EmergencyAgreementVM.EmergencyAgreementSelected == null ||
                EmergencyAgreementVM.EmergencyAgreements.Count == 0)
            {
                new NotificationWindow("Fehler!", "Es sind keine Notrufverträge ausgewählt um zu bearbeiten", null, NotificationWindow.MessageType.Waring).Show();
                return;
            }

            EmergencyAgreementVM.EmergencyAgreementSelectedLast = EmergencyAgreementVM.EmergencyAgreementSelected;
            var result = EmergencyAgreementVM.EditEmergencyAgreement();
            if (result.IsReadOnly)
            {
                AskForceToEdit(result.CurrentlyUsedBy, () =>
                {
                    EmergencyAgreementVM.ForceEditing();
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
            if (EmergencyAgreementVM.EmergencyAgreementSelected.ReadOnly)
            {
                EmergencyAgreementVM.ReleaseEditing();
            }

            EmergencyAgreementVM.EmergencyAgreementSelected = (EmergencyAgreement)dgEmergencyAgreements.SelectedItem;
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
        }
}
