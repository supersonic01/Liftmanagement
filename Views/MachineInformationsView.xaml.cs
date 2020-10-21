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
using Liftmanagement.Models;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for MachineInformationsVIew.xaml
    /// </summary>
    public partial class MachineInformationsView : UserControlView
    {
        public MachineInformationsViewModel MachineInformationsVM { get; set; } = new MachineInformationsViewModel();
        public MachineInformationsView()
        {
            InitializeComponent();
            
            MachineInformationsVM.Refresh();
            NotVisibleColumns.Add(nameof(Location.GoogleDriveFolderName));

            dgMachineInformations.Tag = NotVisibleColumns;

            BindingControl(dgMachineInformations, () => MachineInformationsVM.MachineInformations);

            txtExpanderHeader.Text = new CategoryViewModel().Categories.Where(c => c.MangementType == Helper.Helper.TTypeMangement.Location).Select(c => c.Name).FirstOrDefault();
            expanderMachineInformations.Expanded += ExpanderMachineInformations_Expanded;
            expanderMachineInformations.Collapsed += ExpanderMachineInformations_Collapsed;
        }


        public void ExpanderMachineInformations_Expanded(object sender, RoutedEventArgs e)
        {
            dgMachineInformations.Width = dgMachineInformations.ActualWidth;
        }

        private void ExpanderMachineInformations_Collapsed(object sender, RoutedEventArgs e)
        {
            gridMachineInformations.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
        }

        private void gspLocatios_OnDragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            dgMachineInformations.Width = double.NaN;
        }
    }
}
