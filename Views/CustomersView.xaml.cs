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
    public partial class CustomersView : UserControl
    {
        private CustomersViewModel  customersVM = new CustomersViewModel() ;

        public CustomersViewModel CustomersVM { 
            get { return customersVM; }
            set { customersVM = value; }
        }

        public CustomersView()
        {
            InitializeComponent();

            Binding binding = new Binding("CustomersVM.Customers")
            {
                Source = this
            };

            dgCustomers.SetBinding(DataGrid.ItemsSourceProperty, binding);

            txtExpanderHeader.Text = new CategoryViewModel().Categories.Where(c => c.MangementType == Helper.Helper.TTypeMangement.Customer).Select(c => c.Name).FirstOrDefault();
            expanderCustomers.Expanded += ExpanderCustomers_Expanded;
        }

        private void ExpanderCustomers_Expanded(object sender, RoutedEventArgs e)
        {
            spCustomers.Width = spCustomers .ActualWidth;       
        }
    }
}
