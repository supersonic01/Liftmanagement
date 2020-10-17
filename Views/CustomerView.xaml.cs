using System;
using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using Liftmanagement.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using Liftmanagement.Common;

namespace Liftmanagement.View
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : GoogleDriveDialogueView
    {
        private CustomersView customersView;

        public CustomerViewModel CustomerVM { get; set; } = new CustomerViewModel();

        protected override string ViewModelName => nameof(CustomerVM);
        protected override string SourceObjectStringName => nameof(CustomerVM.CustomerSelected);
        private ContactPartner contactPartner = null;
        private LocationDetailView location;

        public CustomerView()
        {
            InitializeComponent();

            LoadingIndicatorPanel.Visibility = Visibility.Visible;
            MainContent.IsEnabled = false;
            Task.Factory.StartNew(() =>
            {
                var customersVM = new CustomersViewModel();
               
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() =>
                    {
                        customersView = new CustomersView();
                        customersView.CustomersVM = customersVM;
                        frameCustomers.Content = customersView;
                        customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;
                        AssigneValuesToControl();
                        customersView.dgCustomers.SelectedIndex = 0;
                    }));



            }).ContinueWith((task) =>
            {
                LoadingIndicatorPanel.Visibility = Visibility.Collapsed;
                MainContent.IsEnabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());

            //TODO Add refresh button to refrexh all data
        }

        

        private void CustomerView_Loaded(object sender, RoutedEventArgs e)
        {
            AssigneValuesToControl();
        }

        private void DgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = GetSelectedCustomer(sender);

            if (customer == null)
            {
                return;

            }

            EnableContoles(false);

            CustomerVM.CustomerSelected = customer;


            dgAdministratorContactPersons.SelectionChanged -= DgAdministratorContactPersons_SelectionChanged;
            BindingControl(dgAdministratorContactPersons, () => CustomerVM.CustomerSelected.Administrator.ContactPersons);
            //dgAdministratorContactPersons.ItemsSource = customer.Administrator.ContactPersons;
            dgAdministratorContactPersons.SelectionChanged += DgAdministratorContactPersons_SelectionChanged;
            dgAdministratorContactPersons.SelectedIndex = 0;
            
            location = new LocationDetailView(customer);
            frameLocations.Content = location;

        }

        private void AssigneValuesToControl()
        {
            lblCompanyName.Content = CustomerVM.CustomerSelected.GetDisplayName<Customer>(nameof(CustomerVM.CustomerSelected.CompanyName)) + ":";
            lblContactPerson.Content = CustomerVM.CustomerSelected.GetDisplayName<ContactPartner>(nameof(CustomerVM.CustomerSelected.ContactPerson.Name)) + ":";
            lblAddress.Content = CustomerVM.CustomerSelected.GetDisplayName<Customer>(nameof(CustomerVM.CustomerSelected.Address)) + ":";
            lblPostcode.Content = CustomerVM.CustomerSelected.GetDisplayName<Customer>(nameof(CustomerVM.CustomerSelected.Postcode)) + ":";
            lblCity.Content = CustomerVM.CustomerSelected.GetDisplayName<Customer>(nameof(CustomerVM.CustomerSelected.City)) + ":";
            lblPhoneWork.Content = CustomerVM.CustomerSelected.GetDisplayName<ContactPartner>(nameof(CustomerVM.CustomerSelected.ContactPerson.PhoneWork)) + ":";
            lblMobile.Content = CustomerVM.CustomerSelected.GetDisplayName<ContactPartner>(nameof(CustomerVM.CustomerSelected.ContactPerson.Mobile)) + ":";
            lblEmail.Content = CustomerVM.CustomerSelected.GetDisplayName<ContactPartner>(nameof(CustomerVM.CustomerSelected.ContactPerson.EMail)) + ":";
            lblAdditionalInfo.Content = CustomerVM.CustomerSelected.GetDisplayName<Customer>(nameof(CustomerVM.CustomerSelected.AdditionalInfo)) + ":";
            lblGoogleDriveLink.Content = CustomerVM.CustomerSelected.GetDisplayName<Customer>(nameof(CustomerVM.CustomerSelected.GoogleDriveFolderName)) + ":";

            lblAdministratorCompanyName.Content =
                CustomerVM.CustomerSelected.GetDisplayName<AdministratorCompany>(nameof(CustomerVM.CustomerSelected.Administrator.Name)) + ":";
            lblAdministratorContactPerson.Content =
                CustomerVM.CustomerSelected.GetDisplayName<ContactPartner>(nameof(CustomerVM.CustomerSelected.ContactPerson.Name)) + ":";
            lblAdministratorPhoneWork.Content =
                CustomerVM.CustomerSelected.GetDisplayName<ContactPartner>(nameof(CustomerVM.CustomerSelected.ContactPerson.PhoneWork)) + ":";
            lblAdministratorMobile.Content =
                CustomerVM.CustomerSelected.GetDisplayName<ContactPartner>(nameof(CustomerVM.CustomerSelected.ContactPerson.Mobile)) + ":";
            lblAdministratorEmail.Content = CustomerVM.CustomerSelected.GetDisplayName<ContactPartner>(nameof(CustomerVM.CustomerSelected.ContactPerson.EMail)) + ":";


            BindingText(txtCompanyName, ()=>CustomerVM.CustomerSelected.CompanyName,true,()=>new MandatoryRule());
            BindingText(txtContactPerson, () => CustomerVM.CustomerSelected.ContactPerson.Name);
            BindingText(txtAddress, () => CustomerVM.CustomerSelected.Address);
            BindingText(txtPostcode, () => CustomerVM.CustomerSelected.Postcode);
            BindingText(txtCity, () => CustomerVM.CustomerSelected.City);
            BindingText(txtPhoneWork, () => CustomerVM.CustomerSelected.ContactPerson.PhoneWork);
            BindingText(txtMobile, () => CustomerVM.CustomerSelected.ContactPerson.Mobile);
            BindingText(txtEmail, () => CustomerVM.CustomerSelected.ContactPerson.EMail);
            BindingText(txtAdditionalInfo, nameof(CustomerVM.CustomerSelected.AdditionalInfo));
            BindingText(txtAdministratorCompanyName, () => CustomerVM.CustomerSelected.Administrator.Name);
            BindingHyperlink(hyperlinkGoogleDrive, GetPropertyPath(() => CustomerVM.CustomerSelected.GoogleDriveLink));
            BindingTextBlock(txtGoogleDriveFolderName, GetPropertyPath(() => CustomerVM.CustomerSelected.GoogleDriveFolderName));

            BindingText(txtAdministratorContactPerson, () => CustomerVM.AdministratorContactPerson.Name);
            BindingText(txtAdministratorPhoneWork, () => CustomerVM.AdministratorContactPerson.PhoneWork);
            BindingText(txtAdministratorMobile, () => CustomerVM.AdministratorContactPerson.Mobile);
            BindingText(txtAdministratorEmail, () => CustomerVM.AdministratorContactPerson.EMail);
            BindingCheckBox(cboxAdministratorContactByDefect, () => CustomerVM.AdministratorContactPerson.ContactByDefect);
        }


        private void DgAdministratorContactPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dgAdministratorContactPersons = sender as DataGrid;
            if (dgAdministratorContactPersons != null && dgAdministratorContactPersons.SelectedItem != null)
                CustomerVM.AdministratorContactPerson = dgAdministratorContactPersons.SelectedItem as ContactPartner;
        }

        private static Customer GetSelectedCustomer(object sender)
        {
            Customer customer = null;
            var dgCustomers = sender as DataGrid;
            if (dgCustomers != null)
                customer = dgCustomers.SelectedItem as Customer;

            return customer;
        }

        protected override void BtnSaveGoogleDrive_Click(object sender, RoutedEventArgs e)
        {
            var node = googlDriveTree.GetSelectedNode();

            CustomerVM.CustomerSelected.GoogleDriveLink = node.WebLink;
            CustomerVM.CustomerSelected.GoogleDriveFolderName = node.Name;

            windowGoogleDriveTree.Hide();
        }

        private void btnGoogleDriveTree_Click(object sender, RoutedEventArgs e)
        {
            var customer = customersView.dgCustomers.SelectedItem as Customer;
            if (customer != null)
            {
                windowGoogleDriveTree.Title = windowGoogleDriveTree.Title + "  " + customer.CompanyName;
            }

            windowGoogleDriveTree.ShowDialog();
        }
        
        private void ForceValidation()
        {
            txtCompanyName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ForceValidation();

            if (string.IsNullOrWhiteSpace(CustomerVM.CustomerSelected.CompanyName))
            {
                var msg = "Kundenname darf nicht leer sein.";
                new NotificationWindow("Fehler!", msg).Show();
                return;
            }

            if (!string.IsNullOrWhiteSpace(CustomerVM.AdministratorContactPerson.Name) && 
                CustomerVM.AdministratorContactPerson.Id < 0 && CustomerVM.AdministratorContactPerson.Id > -10)
            {
                    CustomerVM.CustomerSelected.Administrator.ContactPersons.Add(CustomerVM.AdministratorContactPerson);
            }

            var result = CustomerVM.Add();
            if (result.Records > 0)
            {
                customersView.CustomersVM.RefreshCustomers();
                var customer = customersView.dgCustomers.Items.Cast<Customer>().Single(c => c.Id == result.Id);
                customersView.dgCustomers.SelectedItem = customer;

                var titel = string.Format("Kunde : {0}", customer.GetFullName());
                var msg = "Kundendaten wurden gespeichert.";
                new NotificationWindow(titel, msg).Show();
            }
            else
            {
                var msg = "Kundendaten konnten nicht gespeichert werden.";
                new NotificationWindow("Fehler!", msg).Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CustomerVM.CustomerSelectedLast = CustomerVM.CustomerSelected;
            CustomerVM.CustomerSelected = new Customer();
            CustomerVM.AdministratorContactPerson = new ContactPartner();

            EnableContoles(true);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            CustomerVM.CustomerSelectedLast = CustomerVM.CustomerSelected;
            var result = CustomerVM.EditCustomer();
            if (result.IsReadOnly)
            {
                AskForceToEdit(result.CurrentlyUsedBy, () =>
                {
                    CustomerVM.ForceEditing();
                    EnableContoles(true);
                });
            }
            else
            {
                EnableContoles(true);
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void btnAddAdminContact_Click(object sender, RoutedEventArgs e)
        {
            CustomerVM.AdministratorContactPerson = new ContactPartner();
        }

        private void btnEditAdminContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveAdminContact_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CustomerVM.AdministratorContactPerson.Name))
            {
                var msg = "Ansprechpartner darf nicht leer sein.";
                new NotificationWindow("Fehler!", msg).Show();
                return;
              
            }

           
            if (CustomerVM.AdministratorContactPerson.Id < 0 && CustomerVM.AdministratorContactPerson.Id > -10)
            {
                CustomerVM.AdministratorContactPerson.Id = new Random().Next(-100, -10);

                CustomerVM.CustomerSelected.Administrator.ContactPersons.Add(CustomerVM.AdministratorContactPerson);
               
            }
            else
            {
                CustomerVM.CustomerSelected.Administrator.ContactPersons.Remove(
                    CustomerVM.AdministratorContactPerson);

                CustomerVM.CustomerSelected.Administrator.ContactPersons.Add(CustomerVM.AdministratorContactPerson);
            }

            dgAdministratorContactPersons.Items.Refresh();

            CustomerVM.AdministratorContactPerson = new ContactPartner();


        }

        private void btnRemoveAdminContact_Click(object sender, RoutedEventArgs e)
        {
            CustomerVM.CustomerSelected.Administrator.ContactPersons.Remove(
                CustomerVM.AdministratorContactPerson);

            CustomerVM.CustomerSelected.Administrator.DeletedContactPersons.Add(
                CustomerVM.AdministratorContactPerson);

            CustomerVM.AdministratorContactPerson = new ContactPartner();

            dgAdministratorContactPersons.Items.Refresh();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerVM.CustomerSelected.ReadOnly)
            {
                CustomerVM.ReleaseEditing();
            }

            CustomerVM.CustomerSelected =(Customer) customersView.dgCustomers.SelectedItem;
            EnableContoles(false);

        }
    }
}
