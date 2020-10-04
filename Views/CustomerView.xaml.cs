using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using Liftmanagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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


            //Binding binding = new Binding("CustomerVM.LocationDetailViews")
            //{
            //    Source = this
            //};

            //gridLocation.SetBinding(ListBox.ItemsSourceProperty, binding);

            //TODO tab order from Email to Addtitional info not working
        }

        private void CustomerView_Loaded(object sender, RoutedEventArgs e)
        {
            AssigneValuesToControl();
        }

        //protected override void EnableContoles(bool enable)
        //{
        //    base.EnableContoles(enable);

        //    //TODO move this to base clase, bind all to one prop
        //   // cboxContactByDefect.IsEnabled = enable;
        //    //btnSave.IsEnabled = enable;
        //    //btnGoogleDriveTree.IsEnabled = enable;

          

        //}

        //private void EnableContoles(bool enable)
        //{

        //    foreach (var textBox in TextBoxes)
        //    {
        //        textBox.IsEnabled = enable;
        //    }

        //    cboxContactByDefect.IsEnabled = enable;
        //    btnSave.IsEnabled = enable;
        //    btnGoogleDriveTree.IsEnabled = enable;

        //    Uri iconUri = null;
        //    if (enable)
        //    {
        //        iconUri = new Uri("pack://application:,,,../Resources/Images/Icons/Custom-Icon-Design-Flatastic-10-Edit-validated.ico", UriKind.RelativeOrAbsolute);
        //    }
        //    else
        //    {
        //        iconUri = new Uri("pack://application:,,,../Resources/Images/Icons/Custom-Icon-Design-Flatastic-10-Edit-not-validated.ico", UriKind.RelativeOrAbsolute);
        //    }

        //    imageIsEnabled.Source = new BitmapImage(iconUri);
        //}

      
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

            //var dd = FindVisualChildren<TextBlock>(this.Content);
            //foreach (TextBlock tb in dd)
            //{nameof(customer.
            //    // do something with tb here
            //    tb.IsEnabled = false;
            //}

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
            // BindingText(txtAdministratorCompanyName, nameof( CustomerVM.CustomerSelected.Administrator.Name));
            //BindingText(txtAdministratorCompanyName, GetFullPath(() => CustomerVM.CustomerSelected.Administrator.Name));
            BindingText(txtAdministratorCompanyName, () => CustomerVM.CustomerSelected.Administrator.Name);
            //BindingText(txtAdministratorCompanyName, "CustomerVM.CustomerSelected.Administrator.Name");
            // BindingText(txtAdministratorCompanyName, "Administrator.Name");
            // var fullPath = GetFullPath(() => CustomerVM.CustomerSelected.Administrator.Name);

            BindingHyperlink(hyperlinkGoogleDrive, GetPropertyPath(() => CustomerVM.CustomerSelected.GoogleDriveLink));
            BindingTextBlock(txtGoogleDriveFolderName, GetPropertyPath(() => CustomerVM.CustomerSelected.GoogleDriveFolderName));
        }


        private void DgAdministratorContactPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO Administrator list do not expand if no elements inside
         
            var dgAdministratorContactPersons = sender as DataGrid;
            if (dgAdministratorContactPersons != null && dgAdministratorContactPersons.SelectedItem != null)
                contactPartner = dgAdministratorContactPersons.SelectedItem as ContactPartner;

         
            //txtAdministratorContactPerson.Text = contactPartner.Name;
            //txtAdministratorPhoneWork.Text = contactPartner.PhoneWork;
            //txtAdministratorMobile.Text = contactPartner.Mobile;
            //txtAdministratorEmail.Text = contactPartner.EMail;

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
            //TODO Clean if not needed
            var node = googlDriveTree.GetSelectedNode();
            //hyperlinkGoogleDrive.NavigateUri = new Uri(node.WebLink);
            //txtGoogleDriveFolderName.Text = node.Name;


            //var customer = customersView.dgCustomers.SelectedItem as Customer;
            //if (customer != null)
            //{
            //    customer.GoogleDriveLink = node.WebLink;
            //    customer.GoogleDriveFolderName = node.Name;
            //}

            CustomerVM.CustomerSelected.GoogleDriveLink = node.WebLink;
            CustomerVM.CustomerSelected.GoogleDriveFolderName = node.Name;

            // windowGoogleDriveTree.Close();
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
