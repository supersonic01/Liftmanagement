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
using Liftmanagement.Models;
using Liftmanagement.ViewModels;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for MaintenanceAgreementContentView.xaml
    /// </summary>
    public partial class MaintenanceAgreementContentView : UserControlView
    {
        private MaintenanceAgreementContent maintenanceAgreementContentSelected;
        public MaintenanceAgreementContentViewModel MaintenanceAgreementContentVM { get; set; } = new MaintenanceAgreementContentViewModel();

        public MaintenanceAgreementContentView()
        {
            InitializeComponent();
            // AssigneValuesToControl();
        // MaintenanceAgreementContentVM.Refresh(0);
           //dgMaintenanceAgreementContent.ItemsSource = MaintenanceAgreementContentVM.MaintenanceAgreementContents;

            BindingControl(dgMaintenanceAgreementContent,()=>MaintenanceAgreementContentVM.MaintenanceAgreementContents);

            var binding = new Binding("Provide");
            dgCombo.SelectedItemBinding = binding;
            //dgTxt.Binding= new Binding("Content");
            //dgTxt.Header = "Wartungsvertrag Inhalt";

           // this.SizeChanged += MaintenanceAgreementContentView_SizeChanged;

         

            dgCombo.ItemsSource = MaintenanceAgreementContentVM.Provides;
            dgMaintenanceAgreementContent.SelectionChanged += DgMaintenanceAgreementContent_SelectionChanged;
          //  dgMaintenanceAgreementContent.PreviewKeyDown += DgMaintenanceAgreementContent_PreviewKeyDown;

        }

        private void MaintenanceAgreementContentView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newWindowHeight = e.NewSize.Height;
            double newWindowWidth = e.NewSize.Width;
            double prevWindowHeight = e.PreviousSize.Height;
            double prevWindowWidth = e.PreviousSize.Width;

            Console.WriteLine("newWindowHeight : " + newWindowHeight);
            Console.WriteLine("newWindowWidth : " + newWindowWidth);
            Console.WriteLine("prevWindowHeight : " + prevWindowHeight);
            Console.WriteLine("prevWindowWidth : " + prevWindowWidth);
        }

        private void DgMaintenanceAgreementContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dgMaintenanceAgreementContent);
                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        maintenanceAgreementContentSelected = single_row.DataContext as MaintenanceAgreementContent;
                    }
                }

            }
            catch { }
        }

        private void DgMaintenanceAgreementContent_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Tab)
            {
                AddMaintenanceAgreementContent(e);
            }
        }

        private void AddMaintenanceAgreementContent(RoutedEventArgs e)
        {
            if (dgMaintenanceAgreementContent.SelectedIndex == dgMaintenanceAgreementContent.Items.Count - 1)
            {
                MaintenanceAgreementContentVM.MaintenanceAgreementContents.Add(new MaintenanceAgreementContent());
                e.Handled = true;
            }

            dgMaintenanceAgreementContent.CurrentCell = new DataGridCellInfo(
                dgMaintenanceAgreementContent.Items[dgMaintenanceAgreementContent.SelectedIndex + 1], dgMaintenanceAgreementContent.Columns[0]);
            dgMaintenanceAgreementContent.BeginEdit();
        }

        private void Click_Add_MaintenanceAgreementContent(object sender, RoutedEventArgs e)
        {
            AddMaintenanceAgreementContent(e);
        }

        private void Click_Delete_MaintenanceAgreementContent(object sender, RoutedEventArgs e)
        {
            MaintenanceAgreementContentVM.MaintenanceAgreementContents.Remove(maintenanceAgreementContentSelected);
        }

        private void CxmOpened(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddMaintenanceAgreementContent(e);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceAgreementContentVM.MaintenanceAgreementContents.Remove(maintenanceAgreementContentSelected);
        }

        private void btnRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceAgreementContentVM.Refresh(-1);
        }
    }
}
