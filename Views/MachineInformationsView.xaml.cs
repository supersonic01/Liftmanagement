﻿using Liftmanagement.ViewModels;
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
    /// Interaction logic for MachineInformationsVIew.xaml
    /// </summary>
    public partial class MachineInformationsView : UserControl
    {
        private MachineInformationsViewModel machineInformationsVM = new MachineInformationsViewModel();
        public List<string> NotVisibleColumns { get; set; } = new List<string>();
        // public string NotVisibleColumns { get; set; } = "ich nicht";

        public MachineInformationsViewModel MachineInformationsVM
        {
            get { return machineInformationsVM; }
            set { machineInformationsVM = value; }
        }

        public string ThirdColumnTitle { get; set; }
        public string SecondColumnTitle { get; set; }

        public MachineInformationsView()
        {
            InitializeComponent();

            Binding binding = new Binding("MachineInformationsVM.MachineInformations")
            {
                Source = this
            };

            dgMachineInformations.SetBinding(DataGrid.ItemsSourceProperty, binding);

            dgMachineInformations.Tag = NotVisibleColumns;
            dgMachineInformations.AutoGeneratedColumns += DgMachineInformations_AutoGeneratedColumns;


        }

        private void DgMachineInformations_AutoGeneratedColumns(object sender, EventArgs e)
        {
            var visibleColumns = dgMachineInformations.Columns.Where(c => c.Visibility != Visibility.Collapsed);

            if (visibleColumns == null || visibleColumns.Count() <= 0)
            {
                return;
            }

            visibleColumns.Last().Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
    }
}
