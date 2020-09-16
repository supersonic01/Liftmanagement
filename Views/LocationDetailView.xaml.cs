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

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for LocationDetailView.xaml
    /// </summary>
    public partial class LocationDetailView : UserControl
    {
        public LocationDetailView(Location location)
        {
            InitializeComponent();

            if (location == null)
            {
                return;
            }

            lblLocationAdditionalInfo.Content = location.GetDisplayName<Location>(nameof(location.AdditionalInfo)) + ":";
            lblLocationContactPerson.Content = location.GetDisplayName<Location>(nameof(location.ContactPerson)) + ":";
            lblLocationAddress.Content = location.GetDisplayName<Location>(nameof(location.Address)) + ":";
            lblLocationPostcode.Content = location.GetDisplayName<Location>(nameof(location.Postcode)) + ":";
            lblLocationCity.Content = location.GetDisplayName<Location>(nameof(location.City)) + ":";
            lblLocationPhoneWork.Content = location.GetDisplayName<Location>(nameof(location.PhoneWork)) + ":";
            lblLocationMobile.Content = location.GetDisplayName<Location>(nameof(location.Mobile)) + ":";

            txtLocationAdditionalInfo.Text = location.AdditionalInfo;
            txtLocationContactPerson.Text = location.ContactPerson;
            txtLocationAddress.Text = location.Address;
            txtLocationPostcode.Text = location.Postcode;
            txtLocationCity.Text = location.City;
            txtLocationPhoneWork.Text = location.PhoneWork;
            txtLocationMobile.Text = location.Mobile;
        }
    }
}
