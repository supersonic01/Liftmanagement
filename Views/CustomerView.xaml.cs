using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using Liftmanagement.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            customersView = new CustomersView();
            frameCustomers.Content = customersView;
            customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;
            this.Loaded += CustomerView_Loaded;
            customersView.dgCustomers.SelectedIndex = 0;

            //TODO tab order from Email to Addtitional info not working
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
            BindingControl(dgAdministratorContactPersons,()=> CustomerVM.CustomerSelected.Administrator.ContactPersons);
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

            
            BindingText(txtCompanyName, nameof(CustomerVM.CustomerSelected.CompanyName));
            BindingText(txtContactPerson, () => CustomerVM.CustomerSelected.ContactPerson.Name);
            BindingText(txtAddress, () => CustomerVM.CustomerSelected.Address);
            BindingText(txtPostcode, () => CustomerVM.CustomerSelected.Postcode);
            BindingText(txtCity, () => CustomerVM.CustomerSelected.City);
            BindingText(txtPhoneWork, () => CustomerVM.CustomerSelected.ContactPerson.PhoneWork);
            BindingText(txtMobile,  () =>  CustomerVM.CustomerSelected.ContactPerson.Mobile);
            BindingText(txtEmail, () => CustomerVM.CustomerSelected.ContactPerson.EMail);
            BindingText(txtAdditionalInfo, nameof( CustomerVM.CustomerSelected.AdditionalInfo));
            BindingText(txtAdministratorCompanyName, () => CustomerVM.CustomerSelected.Administrator.Name);
            BindingHyperlink(hyperlinkGoogleDrive, GetPropertyPath(() => CustomerVM.CustomerSelected.GoogleDriveLink));
            BindingTextBlock(txtGoogleDriveFolderName, GetPropertyPath(() => CustomerVM.CustomerSelected.GoogleDriveFolderName));
        }


        private void DgAdministratorContactPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO Administrator list do not expand if no elements inside
         
            var dgAdministratorContactPersons = sender as DataGrid;
            if (dgAdministratorContactPersons != null && dgAdministratorContactPersons.SelectedItem != null)
                contactPartner = dgAdministratorContactPersons.SelectedItem as ContactPartner;

            BindingText(txtAdministratorContactPerson, () => CustomerVM.ContactPerson.Name);
            BindingText(txtAdministratorPhoneWork, () => CustomerVM.ContactPerson.PhoneWork);
            BindingText(txtAdministratorMobile, () => CustomerVM.ContactPerson.Mobile);
            BindingText(txtAdministratorEmail, () => CustomerVM.ContactPerson.EMail);
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

        private void btnTakeOverToLocation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //TODO Validate Textbox

            if(!string.IsNullOrWhiteSpace(CustomerVM.ContactPerson.Name))
            {
                CustomerVM.CustomerSelected.Administrator.ContactPersons.Add(CustomerVM.ContactPerson);
            }
           
            var result =  CustomerVM.Add();
            if (result.Records > 0)
            {
                //TODO show Toast msg
                customersView.CustomersVM.RefreshCustomers();
                var customer = customersView.dgCustomers.Items.Cast<Customer>().Single(c => c.Id == result.Id);
                customersView.dgCustomers.SelectedItem = customer;
            }
            else
            {
                //TODO show Toast msg
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CustomerVM.CustomerSelected = new Customer();
            EnableContoles(true);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

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

       
    }
}
