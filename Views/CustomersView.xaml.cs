using Liftmanagement.Helper;
using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for CustomersView.xaml
    /// </summary>
    public partial class CustomersView : UserControlView
    {
        private CustomersViewModel customersVM = null;// new CustomersViewModel() ;

        public CustomersViewModel CustomersVM { 
            get { return customersVM; }
            set { SetField(ref customersVM, value); }
        }

        public CustomersView()
        {
            InitializeComponent();

            NotVisibleColumns.Add(nameof(Customer.Address));
            NotVisibleColumns.Add(nameof(Customer.AdditionalInfo));
            NotVisibleColumns.Add(nameof(Customer.GoogleDriveFolderName));
            NotVisibleColumns.Add(nameof(Customer.Administrator));

            dgCustomers.Tag = NotVisibleColumns;

            BindingControl(dgCustomers, () => CustomersVM.Customers);

            txtExpanderHeader.Text = new CategoryViewModel().Categories.Where(c => c.MangementType == Helper.Helper.TTypeMangement.Customer).Select(c => c.Name).FirstOrDefault();
            expanderCustomers.Expanded += ExpanderCustomers_Expanded;
            expanderCustomers.Collapsed += ExpanderCustomers_Collapsed;

        }

        public void ExpanderCustomers_Expanded(object sender, RoutedEventArgs e)
        {         
            dgCustomers.Width = dgCustomers.ActualWidth;
        }

        private void GspCustomer_OnDragStarted(object sender, DragStartedEventArgs e)
        {
            dgCustomers.Width=double.NaN;
        }

        private void ExpanderCustomers_Collapsed(object sender, RoutedEventArgs e)
        {
            gridCustomers.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }
    }
}
