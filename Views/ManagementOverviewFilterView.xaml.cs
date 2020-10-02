using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Google.Apis.Drive.v3.Data;
using Liftmanagement.Models;
using Liftmanagement.ViewModels;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for ManagementOverviewFilterView.xaml
    /// </summary>
    public partial class ManagementOverviewFilterView : UserControlView
    {
        public ManagementOverviewFilterViewModel ManagementOverviewFilterVM { get; set; } = new ManagementOverviewFilterViewModel();

        protected override string ViewModelName
        {
            get
            {
                return nameof(ManagementOverviewFilterVM);
            }
        }

        public ManagementOverviewFilterView()
        {
            InitializeComponent();

            BindingControl(dgOverviewFilter, nameof(ManagementOverviewFilterVM.ManagementOverviewView));
        }

        public void SetFilter(string filter)
        {

            var yourCostumFilter = new Predicate<object>(item => ((ManagementOverviewFilter)item).FullName.Contains(filter));

            ManagementOverviewFilterVM.ManagementOverviewView.Filter = yourCostumFilter;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        //public void SetDoubleClickEventHandler(EventHandler<MouseButtonEventArgs> eventTarget, EventHandler<KeyEventArgs> eventTargetKey)
        public void SetDoubleClickEventHandler(EventHandler<MouseButtonEventArgs> eventTarget)
        {
            //Style rowStyle = new Style(typeof(DataGridRow));
           
            dgOverviewFilter.RowStyle.Setters.Add(new EventSetter(DataGridRow.MouseDoubleClickEvent,
                new MouseButtonEventHandler(eventTarget)));

            //dgOverviewFilter.RowStyle.Setters.Add(new EventSetter(DataGridRow.KeyUpEvent,
            //    new KeyEventHandler(eventTargetKey)));

            // dgOverviewFilter.RowStyle = rowStyle;
        }

    }
}
