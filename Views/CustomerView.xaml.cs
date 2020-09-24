using Liftmanagement.Helper;
using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using Liftmanagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting;
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

namespace Liftmanagement.View
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : GoogleDriveDialogueView
    {
        private CustomersView customersView;

        public CustomerViewModel CustomerVM { get; set; } = new CustomerViewModel();
        
        public CustomerView()
        {
            InitializeComponent();

            customersView = new CustomersView();
            frameCustomers.Content = customersView;
            customersView.expanderCustomers.Collapsed += ExpanderCustomers_Collapsed;
            customersView.dgCustomers.SelectionChanged += DgCustomers_SelectionChanged;
            customersView.spCustomers.Loaded += SpCustomers_Loaded;
            customersView.dgCustomers.SelectedIndex = 0;
           

            //Binding binding = new Binding("CustomerVM.LocationDetailViews")
            //{
            //    Source = this
            //};

            //gridLocation.SetBinding(ListBox.ItemsSourceProperty, binding);
           
        }


        private void SpCustomers_Loaded(object sender, RoutedEventArgs e)
        {
            var spCustomers = sender as StackPanel;
            if (spCustomers == null)
                return;

            //Work around
            Console.WriteLine(string.Format("sp: {0} ", spCustomers.Width));
            spCustomers.Width = 451;
            spCustomers.Width = 450;
        }

        private void DgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = GetSelectedCustomer(sender);

            if (customer == null)
            {
                return;

            }

            Console.WriteLine("Customer :" + customer.Address);

            lblCompanyName.Content = customer.GetDisplayName<Customer>(nameof(customer.CompanyName)) + ":";
            lblContactPerson.Content = customer.GetDisplayName<ContactPartner>(nameof(customer.ContactPerson.Name)) + ":";
            lblAddress.Content = customer.GetDisplayName<Customer>(nameof(customer.Address)) + ":";
            lblPostcode.Content = customer.GetDisplayName<Customer>(nameof(customer.Postcode)) + ":";
            lblCity.Content = customer.GetDisplayName<Customer>(nameof(customer.City)) + ":";
            lblPhoneWork.Content = customer.GetDisplayName<ContactPartner>(nameof(customer.ContactPerson.PhoneWork)) + ":";
            lblMobile.Content = customer.GetDisplayName<ContactPartner>(nameof(customer.ContactPerson.Mobile)) + ":";
            lblEmail.Content = customer.GetDisplayName<ContactPartner>(nameof(customer.ContactPerson.EMail)) + ":";
            lblAdditionalInfo.Content = customer.GetDisplayName<Customer>(nameof(customer.AdditionalInfo)) + ":";
            lblGoogleDriveLink.Content= customer.GetDisplayName<Customer>(nameof(customer.GoogleDriveFolderName)) + ":";

            lblAdministratorCompanyName.Content = customer.GetDisplayName<AdministratorCompany>(nameof(customer.Administrator.Name)) + ":";
            lblAdministratorContactPerson.Content = customer.GetDisplayName<ContactPartner>(nameof(customer.ContactPerson.Name)) + ":";
            lblAdministratorPhoneWork.Content = customer.GetDisplayName<ContactPartner>(nameof(customer.ContactPerson.PhoneWork)) + ":";
            lblAdministratorMobile.Content = customer.GetDisplayName<ContactPartner>(nameof(customer.ContactPerson.Mobile)) + ":";
            lblAdministratorEmail.Content = customer.GetDisplayName<ContactPartner>(nameof(customer.ContactPerson.EMail)) + ":";


            txtCompanyName.Text = customer.CompanyName;
            txtContactPerson.Text = customer.ContactPerson.Name;
            txtAddress.Text = customer.Address;
            txtPostcode.Text = customer.Postcode;
            txtCity.Text = customer.City;
            txtPhoneWork.Text = customer.ContactPerson.PhoneWork;
            txtMobile.Text = customer.ContactPerson.Mobile;
            txtAdditionalInfo.Text = customer.AdditionalInfo;
            txtAdministratorCompanyName.Text = customer.Administrator.Name;

            dgAdministratorContactPersons.SelectionChanged -= DgAdministratorContactPersons_SelectionChanged;
            dgAdministratorContactPersons.ItemsSource = customer.Administrator.ContactPersons;            
            dgAdministratorContactPersons.SelectionChanged += DgAdministratorContactPersons_SelectionChanged;
            dgAdministratorContactPersons.SelectedIndex = 0;


            List<Location> locations = TestData.GetLocations();

            //  var location = locations.Where(c => c.CustomerId == customer.CustomerId).FirstOrDefault();

            ObservableCollection<LocationDetailView> locationDetailViews = new ObservableCollection<LocationDetailView>();
            locations.ForEach(c => locationDetailViews.Add(new LocationDetailView(c)));
            locations.ForEach(c => locationDetailViews.Add(new LocationDetailView(c)));
            //  CustomerVM.LocationDetailViews = locationDetailViews;
            //gridLocation.ItemsSource = CustomerVM.LocationDetailViews;

        }


        private void DgAdministratorContactPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContactPartner contactPartner = null;
            var dgAdministratorContactPersons = sender as DataGrid;
            if (dgAdministratorContactPersons != null)
                contactPartner = dgAdministratorContactPersons.SelectedItem as ContactPartner;

            if (contactPartner == null)
            {
                return;

            }

            txtAdministratorContactPerson.Text = contactPartner.Name;
            txtAdministratorPhoneWork.Text = contactPartner.PhoneWork;
            txtAdministratorMobile.Text = contactPartner.Mobile;
            txtAdministratorEmail.Text = contactPartner.EMail;

        }

        private static Customer GetSelectedCustomer(object sender)
        {
            Customer customer = null;
            var dgCustomers = sender as DataGrid;
            if (dgCustomers != null)
                customer = dgCustomers.SelectedItem as Customer;

            return customer;
        }


        private void ExpanderCustomers_Collapsed(object sender, RoutedEventArgs e)
        {

            Console.WriteLine("expanderCustomers_Collapsed");
            gridResizableCustomers.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }

        
        protected override void BtnSaveGoogleDrive_Click(object sender, RoutedEventArgs e)
        {
            var node = googlDriveTree.GetSelectedNode();
            hyperlinkGoogleDrive.NavigateUri = new Uri(node.WebLink);
            txtGoogleDriveFolderName.Text = node.Name;


            var customer = customersView.dgCustomers.SelectedItem as Customer;
            if (customer != null)
            {
                customer.GoogleDriveLink = node.WebLink;
                customer.GoogleDriveFolderName = node.Name;
            }

            // windowGoogleDriveTree.Close();
            windowGoogleDriveTree.Hide();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var customer = customersView.dgCustomers.SelectedItem as Customer;
            if (customer != null)
            {
                windowGoogleDriveTree.Title = windowGoogleDriveTree.Title + "  " + customer.CompanyName;
            }

            windowGoogleDriveTree.ShowDialog();
        }
    }
}
