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
using System.Windows.Threading;
using Liftmanagement.Common;
using Liftmanagement.ViewModels;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for MachineInformationView.xaml
    /// </summary>
    public partial class MachineInformationView : GoogleDriveDialogueView
    {
        private MasterDataInfoView masterDataInfo;
        private CustomersView customersView;
        public MachineInformationViewModel MachineInformationVM { get; set; } = new MachineInformationViewModel();

        public MachineInformationView()
        {
            InitializeComponent();
            AssignSelectAllForTextBoxes();


            //TODO select only Customer
            //TODO Datetime Validation and format of filed

            LoadingIndicatorPanel.Visibility = Visibility.Visible;
            gridResizableMachineInformation.IsEnabled = false;

            //Task.Factory.StartNew(() =>
            //{
            //    var customersVM = new CustomersViewModel();
            //    customersVM.RefreshOnlyCustomers();

            //    Application.Current.Dispatcher.BeginInvoke(
            //        DispatcherPriority.Background,
            //        new Action(() =>
            //        {
            //            customersView = new CustomersView();
            //            customersView.CustomersVM = customersVM;

            //            frameCustomers.Content = customersView;

            //            var locationsView = new LocationsView();
            //            frameLocations.Content = locationsView;

            //            customersView.expanderCustomers.Collapsed += ExpanderCustomers_Collapsed;
            //            customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;

            //            locationsView.expanderLocations.Collapsed += ExpanderLocations_Collapsed;
            //            locationsView.dgLocations.SelectionChanged += DgLocations_SelectionChanged;

            //        }));

            //});


            Task.Factory.StartNew(() =>
            {
                var customersVM = new CustomersViewModel();
                customersVM.RefreshOnlyCustomers();

                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() =>
                    {
                        customersView = new CustomersView();
                        customersView.CustomersVM = customersVM;

                        frameCustomers.Content = customersView;

                        var locationsView = new LocationsView();
                        frameLocations.Content = locationsView;
                        
                     NotVisibleColumns.Add(nameof(MachineInformation.ContactPerson));

                     dgMachineInformations.SelectionChanged += DgMachineInformations_SelectionChanged;
                        dgMachineInformations.SelectedIndex = 0;

                        AssigneValuesToControl();

                        customersView.expanderCustomers.Collapsed += ExpanderCustomers_Collapsed;
                        customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;

                        locationsView.expanderLocations.Collapsed += ExpanderLocations_Collapsed;
                        locationsView.dgLocations.SelectionChanged += DgLocations_SelectionChanged;

                        masterDataInfo = new MasterDataInfoView(Helper.Helper.TTypeMangement.MachineInformation);
                        masterDataInfo.cbLocations.SelectionChanged += CbLocations_SelectionChanged;
                        masterDataInfo.RefreshCustomers();
                        frameMasterDataInfo.Content = masterDataInfo;

                    }));

            }).ContinueWith((task) =>
            {
                LoadingIndicatorPanel.Visibility = Visibility.Collapsed;
                gridResizableMachineInformation.IsEnabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());

            EnableContoles(false);
        }

        private void CbLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var location = GetSelectedObject<Location>(sender);

            if (location != null)
                MachineInformationVM.RefreshByLocation(location.Id);
        }


        private void DgMachineInformations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MachineInformationVM.MachineInformationSelected = GetSelectedObject<MachineInformation>(sender);
            EnableContoles(false);
        }

        private void AssigneValuesToControl()
        {
            lblName.Content = MachineInformationVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MachineInformationVM.MachineInformationSelected.Name)) + ":";
            lblYearOfConstruction.Content =
                MachineInformationVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MachineInformationVM.MachineInformationSelected.YearOfConstruction)) + ":";
            lblSerialNumber.Content =
                MachineInformationVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MachineInformationVM.MachineInformationSelected.SerialNumber)) + ":";
            lblHoldingPositions.Content =
                MachineInformationVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MachineInformationVM.MachineInformationSelected.HoldingPositions)) + ":";
            lblEntrances.Content = MachineInformationVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MachineInformationVM.MachineInformationSelected.Entrances)) +
                                   ":";
            lblDescription.Content =
                MachineInformationVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MachineInformationVM.MachineInformationSelected.Description)) + ":";
            lblContactPerson.Content =
                MachineInformationVM.MachineInformationSelected.GetDisplayName<ContactPartner>(nameof(MachineInformationVM.MachineInformationSelected.ContactPerson.Name)) + ":";
            lblPhoneWork.Content =
                MachineInformationVM.MachineInformationSelected.GetDisplayName<ContactPartner>(nameof(MachineInformationVM.MachineInformationSelected.ContactPerson.PhoneWork)) + ":";
            lblMobile.Content =
                MachineInformationVM.MachineInformationSelected.GetDisplayName<ContactPartner>(nameof(MachineInformationVM.MachineInformationSelected.ContactPerson.Mobile)) + ":";
            lblContactByDefect.Content =
                MachineInformationVM.MachineInformationSelected.GetDisplayName<ContactPartner>(nameof(MachineInformationVM.MachineInformationSelected.ContactPerson.ContactByDefect)) + ":";

            lblGoogleDriveLink.Content =
                MachineInformationVM.MachineInformationSelected.GetDisplayName<MachineInformation>(nameof(MachineInformationVM.MachineInformationSelected.GoogleDriveFolderName)) + ":";


            BindingText(txtName, () => MachineInformationVM.MachineInformationSelected.Name, null, true, () => new MandatoryRule());
            BindingDatetime(txtYearOfConstruction, ()=>MachineInformationVM.MachineInformationSelected.YearOfConstruction);
            BindingText(txtSerialNumber, () => MachineInformationVM.MachineInformationSelected.SerialNumber, null, true, () => new MandatoryRule());
            BindingText(txtHoldingPositions, () => MachineInformationVM.MachineInformationSelected.HoldingPositions);
            BindingText(txtEntrances, () => MachineInformationVM.MachineInformationSelected.Entrances);
            BindingText(txtDescription, () => MachineInformationVM.MachineInformationSelected.Description);
            BindingText(txtContactPerson, () => MachineInformationVM.MachineInformationSelected.ContactPerson.Name);
            BindingText(txtPhoneWork, () => MachineInformationVM.MachineInformationSelected.ContactPerson.PhoneWork);
            BindingText(txtMobile, () => MachineInformationVM.MachineInformationSelected.ContactPerson.Mobile);
            BindingCheckBox(cbContactByDefect, () => MachineInformationVM.MachineInformationSelected.ContactPerson.ContactByDefect);
            BindingHyperlink(hyperlinkGoogleDrive, GetPropertyPath(() => MachineInformationVM.MachineInformationSelected.GoogleDriveLink));
            BindingTextBlock(txtGoogleDriveFolderName, GetPropertyPath(() => MachineInformationVM.MachineInformationSelected.GoogleDriveFolderName));
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

        private void ExpanderLocations_Collapsed(object sender, RoutedEventArgs e)
        {
            gridResizableMachineInformation.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Auto);
        }

        private void DgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = GetSelectedObject<Customer>(sender);

            if (customer != null)
                masterDataInfo.cbCustomers.SelectedItem = masterDataInfo.cbCustomers.Items.Cast<Customer>().Single(c => c.Id == customer.Id);

        }
        private void ExpanderCustomers_Collapsed(object sender, RoutedEventArgs e)
        {
            gridResizableMachineInformation.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }

        //private void Stackpanel_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var stackpanel = sender as StackPanel;
        //    if (stackpanel == null)
        //        return;

        //    //Work around           
        //    stackpanel.Width = stackpanel.Width + 1;
        //    // stackpanel.Width = 450;
        //}

        protected override void BtnSaveGoogleDrive_Click(object sender, RoutedEventArgs e)
        {
            var node = googlDriveTree.GetSelectedNode();

            MachineInformationVM.MachineInformationSelected.GoogleDriveLink = node.WebLink;
            MachineInformationVM.MachineInformationSelected.GoogleDriveFolderName = node.Name;

            windowGoogleDriveTree.Hide();
        }

        private void btnGoogleDriveTree_Click(object sender, RoutedEventArgs e)
        {
            if (MachineInformationVM.MachineInformationSelected != null)
            {
                windowGoogleDriveTree.Title = windowGoogleDriveTree.Title + "  " + MachineInformationVM.MachineInformationSelected.GetFullName();
            }

            windowGoogleDriveTree.ShowDialog();
        }

        private void ForceValidation()
        {
            txtName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtYearOfConstruction.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtSerialNumber.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                ForceValidation();
                bool isValid = true;
                StringBuilder validationMsg = new StringBuilder();


                if (string.IsNullOrWhiteSpace(MachineInformationVM.MachineInformationSelected.Name))
                {
                    validationMsg.Append("Name darf nicht leer sein.");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(MachineInformationVM.MachineInformationSelected.SerialNumber))
                {
                    validationMsg.AppendLine("Fabriknummer darf nicht leer sein.");
                    isValid = false;
                }

                if (!isValid)
                {
                    new NotificationWindow("Fehler!", validationMsg.ToString()).Show();
                    return;
                }

                var result = MachineInformationVM.Add(masterDataInfo.MasterDataInfoVM.LocationSelected);
                if (result.Records > 0)
                {
                    MachineInformationVM.RefreshByLocation(masterDataInfo.MasterDataInfoVM.LocationSelected.Id);
                    var machineInformation = dgMachineInformations.Items.Cast<MachineInformation>().Single(c => c.Id == result.Id);
                    dgMachineInformations.SelectedItem = machineInformation;

                    var titel = string.Format("Anlage : {0}", machineInformation.GetFullName());
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
                new NotificationWindow("Fehler!", exception.ToString()).Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MachineInformationVM.MachineInformationSelectedLast = MachineInformationVM.MachineInformationSelected;
            MachineInformationVM.MachineInformationSelected = new MachineInformation();

            EnableContoles(true);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            MachineInformationVM.MachineInformationSelectedLast = MachineInformationVM.MachineInformationSelected;
            var result = MachineInformationVM.EditMachineInformation();
            if (result.IsReadOnly)
            {
                AskForceToEdit(result.CurrentlyUsedBy, () =>
                {
                    MachineInformationVM.ForceEditing();
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
            if (MachineInformationVM.MachineInformationSelected.ReadOnly)
            {
                MachineInformationVM.ReleaseEditing();
            }

            MachineInformationVM.MachineInformationSelected = (MachineInformation) dgMachineInformations.SelectedItem;
            EnableContoles(false);

        }

    }
}
