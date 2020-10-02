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
    /// Interaction logic for RcordingView.xaml
    /// </summary>
    public partial class RecordingsView : UserControlView
    {
        public RecordingsViewModel RcordingsVM { get; set; } = new RecordingsViewModel();

        protected override string ViewModelName
        {
            get
            {
                return nameof(RcordingsVM);
            }
        }

        public RecordingsView()
        {
            InitializeComponent();


            BindingControl(dgRecordings, nameof(RcordingsVM.Recordings));

            dgRecordings.SelectionChanged += DgRecordings_SelectionChanged;
            dgRecordings.SelectedIndex = 0;


        }

        private void DgRecordings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recording recording = GetSelectedObject<Recording>(sender);

            if (recording == null)
            {
               

            }
        }

        public Recording GetSelectedItem()
        {
            return dgRecordings.SelectedItem as Recording;
        }

       
    }
}
