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
    public partial class MaintenanceAgreementsView : UserControlView
    {
        private MaintenanceAgreementsViewModel maintenanceAgreementsVM = new MaintenanceAgreementsViewModel();

        public MaintenanceAgreementsViewModel MaintenanceAgreementsVM
        {
            get { return maintenanceAgreementsVM; }
            set { SetField(ref maintenanceAgreementsVM, value); }
        }

        public MaintenanceAgreementsView()
        {
            InitializeComponent();
            dgMaintenanceAgreements.Tag = NotVisibleColumns;

            BindingControl(dgMaintenanceAgreements, () => MaintenanceAgreementsVM.MaintenanceAgreements);
        }
    }
}
