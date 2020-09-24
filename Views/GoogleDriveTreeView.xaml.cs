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
using System.Windows.Threading;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for GoogleDriveTreeView.xaml
    /// </summary>
    public partial class GoogleDriveTreeView : UserControl // Do not inherit from UserControlView
    {

        public GoogleDriveTreeView()
        {
            InitializeComponent();
            this.Loaded += GoogleDriveTreeView_Loaded;
        }

        private void GoogleDriveTreeView_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                var googleDriveTreeViewModel = new GoogleDriveTreeViewModel().Items;

                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() =>
                    {
                        GoogleDriveFolderHierarchy.ItemsSource = googleDriveTreeViewModel;
                    }));
            }).ContinueWith((task) =>
            {
                LoadingIndicatorPanel.Visibility = Visibility.Collapsed;
                MainContent.IsEnabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public GoogleDriveTreeNodeViewModel GetSelectedNode()
        {
            var node = GoogleDriveFolderHierarchy.SelectedItem as GoogleDriveTreeNodeViewModel;
            return node;
        }

        private void btnShowInGoogleDrive_Click(object sender, RoutedEventArgs e)
        {
            var node = GetSelectedNode();
            if (node != null)
            {
                Console.WriteLine(node.WebLink);
                System.Diagnostics.Process.Start(node.WebLink);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            GoogleDriveFolderHierarchy.ItemsSource = new GoogleDriveTreeViewModel().Items;
        }

    }
}
