using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for GoogleDriveTreeView.xaml
    /// </summary>
    public partial class GoogleDriveTreeView : UserControl
    {

        public GoogleDriveTreeView()
        {
            InitializeComponent();

            // binding TreeView component with TreeModel
            GoogleDriveFolderHierarchy.ItemsSource = new GoogleDriveTreeViewModel().Items;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
           var item=  GoogleDriveFolderHierarchy.SelectedItem;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
