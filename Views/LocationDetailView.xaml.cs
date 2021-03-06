﻿using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
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
using Liftmanagement.ViewModels;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for LocationDetailView.xaml
    /// </summary>
    public partial class LocationDetailView : GoogleDriveDialogueView
    {
        private readonly Customer _customer;
        public LocationDetailViewModel LocationDetailVM { get; set; } = new LocationDetailViewModel();
        public LocationsViewModel LocationsVM { get; set; } = new LocationsViewModel();

        //protected override string ViewModelName => nameof(LocationDetailVM);
        //protected override string SourceObjectStringName => nameof(LocationDetailVM.Location);

        public LocationDetailView(Customer customer)
        {
            _customer = customer;
            InitializeComponent();

            Location location = new Location();

            //TODO by SelectedIndex = 0  row will not highlighted
            dgLocations.Columns.Add(new DataGridTextColumn
            {
                Header = location.GetDisplayName<ContactPartner>(nameof(location.ContactPerson.Name)),
                Binding = new Binding("ContactPerson.Name"),
                DisplayIndex = 8
            });

            dgLocations.Columns.Add(new DataGridCheckBoxColumn()
            {
                Header = location.GetDisplayName<ContactPartner>(nameof(location.ContactPerson.ContactByDefect)),
                Binding = new Binding("ContactPerson.ContactByDefect"),
                DisplayIndex = 9
            });


            BindingControl(dgLocations, () => LocationsVM.Locations);

            EnableContoles(false);
            
            SetLable(LocationDetailVM.LocationSelected);

            Task.Factory.StartNew(() =>
            {
                LocationsVM.LocationsByCustomer(_customer);

                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() =>
                    {
                        dgLocations.SelectionChanged += DgLocations_SelectionChanged;

                    }));
            }).ContinueWith((task) =>
            {
                dgLocations.SelectedIndex = 0;
            }, TaskScheduler.FromCurrentSynchronizationContext());

            //TODO check if its faster when put in new Task
           
            Application.Current.MainWindow.SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            gridRdLocations.Height = new GridLength(gridRdLocations.ActualHeight);
        }


        private void LocationDetailView_Loaded(object sender, RoutedEventArgs e)
        {
            //LoadData();
            dgLocations.SelectedIndex = 0;
        }

        private void LoadData()
        {
            LocationsVM.LocationsByCustomer(_customer);

            dgLocations.SelectionChanged += DgLocations_SelectionChanged;
           // dgLocations.SelectedIndex = 0;

            SetLable(LocationDetailVM.LocationSelected);

            EnableContoles(false);
        }


        private void DgLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Location location = GetSelectedObject<Location>(sender);

            if (location != null)
            {
                LocationDetailVM.LocationSelected = location;
            }

            EnableContoles(false);
        }

        private void SetLable(Location location)
        {
            lblAdditionalInfo.Content = location.GetDisplayName<Location>(nameof(location.AdditionalInfo)) + ":";
            lblContactPerson.Content = location.GetDisplayName<ContactPartner>(nameof(location.ContactPerson.Name)) + ":";
            lblAddress.Content = location.GetDisplayName<Location>(nameof(location.Address)) + ":";
            lblPostcode.Content = location.GetDisplayName<Location>(nameof(location.Postcode)) + ":";
            lblCity.Content = location.GetDisplayName<Location>(nameof(location.City)) + ":";
            lblPhoneWork.Content = location.GetDisplayName<ContactPartner>(nameof(location.ContactPerson.PhoneWork)) + ":";
            lblMobile.Content = location.GetDisplayName<ContactPartner>(nameof(location.ContactPerson.Mobile)) + ":";
            lblEmail.Content = location.GetDisplayName<ContactPartner>(nameof(location.ContactPerson.EMail)) + ":";
            lblContactByDefect.Content =
                location.GetDisplayName<ContactPartner>(nameof(location.ContactPerson.ContactByDefect)) + ":";
            lblGoogleDriveLink.Content = location.GetDisplayName<Location>(nameof(location.GoogleDriveFolderName)) + ":";

            BindingControl(this, ItemsControl.DataContextProperty, () => LocationDetailVM.LocationSelected);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_customer != null)
            {
                windowGoogleDriveTree.Title = windowGoogleDriveTree.Title + "  " + _customer.CompanyName +
                                              ", " + LocationDetailVM.LocationSelected.GetFullName();
            }
            windowGoogleDriveTree.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            LocationDetailVM.LocationSelected = new Location();
            EnableContoles(true);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            var isSelected = IsILtemSelected("Standort", () =>
            {
                return (LocationDetailVM.LocationSelected == null || LocationDetailVM.LocationSelected.Id < 0);
            });

            if (!isSelected)
                return;
            
            var result = LocationDetailVM.EditLocation();
            if (result.IsReadOnly)
            {
                AskForceToEdit(result.CurrentlyUsedBy,() =>
                {
                    LocationDetailVM.ForceEditing();
                    EnableContoles(true);
                });
            }
            else
            {
                EnableContoles(true);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MarkForDelete("Standort", LocationDetailVM.LocationSelected,
                () => LocationDetailVM.MarkForDeleteLocation(),
                () => {
                    LocationsVM.LocationsByCustomer(_customer);
                    dgLocations.SelectedIndex = 0;
                });
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var result = LocationDetailVM.Add(_customer);

            if (result.Records > 0)
            {
                LocationsVM.LocationsByCustomer(_customer);
                var location = dgLocations.Items.Cast<Location>().Single(c => c.Id == result.Id);
                dgLocations.SelectedItem = location;

                var titel = string.Format("Standort : {0}", location.GetFullName());
                var msg = "Standortdaten wurden gespeichert.";
                new NotificationWindow(titel, msg).Show();
            }
            else
            {
                var msg = "Standortdaten konnten nicht gespeichert werden.";
                new NotificationWindow("Fehler!", msg).Show();
            }
        }

        protected override void BtnSaveGoogleDrive_Click(object sender, RoutedEventArgs e)
        {
            var node = googlDriveTree.GetSelectedNode();

            LocationDetailVM.LocationSelected.GoogleDriveLink = node.WebLink;
            LocationDetailVM.LocationSelected.GoogleDriveFolderName = node.Name;

            windowGoogleDriveTree.Hide();
        }

        private void btnTakeOverLocationFromCustomer_Click(object sender, RoutedEventArgs e)
        {
            LocationDetailVM.LocationSelected.Address = _customer.Address;
            LocationDetailVM.LocationSelected.Postcode = _customer.Postcode;
            LocationDetailVM.LocationSelected.City = _customer.City;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (LocationDetailVM.LocationSelected.ReadOnly)
            {
                LocationDetailVM.ReleaseEditing();
            }

            LocationDetailVM.LocationSelected = (Location) dgLocations.SelectedItem;
            EnableContoles(false);
        }
    }
}
