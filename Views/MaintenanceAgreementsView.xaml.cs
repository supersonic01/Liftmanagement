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
    /// Interaction logic for MaintenanceAgreementsView.xaml
    /// </summary>
    public partial class MaintenanceAgreementsView : UserControl
    {
        private MaintenanceAgreementsViewModel machineInformationsVM = new MaintenanceAgreementsViewModel();
        public List<string> NotVisibleColumns { get; set; } = new List<string>();
        // public string NotVisibleColumns { get; set; } = "ich nicht";

        public MaintenanceAgreementsViewModel MaintenanceAgreementsVM
        {
            get { return machineInformationsVM; }
            set { machineInformationsVM = value; }
        }

        public string ThirdColumnTitle { get; set; }
        public string SecondColumnTitle { get; set; }

        public MaintenanceAgreementsView()
        {
            InitializeComponent();

            Binding binding = new Binding("MaintenanceAgreementsVM.MaintenanceAgreements")
            {
                Source = this
            };

            dgMaintenanceAgreements.SetBinding(DataGrid.ItemsSourceProperty, binding);
        }
    }
}
