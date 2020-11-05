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
    public partial class RecordsView : UserControlView
    {
        public RecordsViewModel RecordsVM { get; set; } = new RecordsViewModel();

        protected override string ViewModelName
        {
            get
            {
                return nameof(RecordsVM);
            }
        }

        public RecordsView()
        {
            InitializeComponent();


            BindingControl(dgRecords, nameof(RecordsVM.Records));

            dgRecords.SelectionChanged += DgRecords_SelectionChanged;
            dgRecords.SelectedIndex = 0;


        }

        private void DgRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Record record = GetSelectedObject<Record>(sender);

            if (record == null)
            {
               

            }
        }

        public Record GetSelectedItem()
        {
            return dgRecords.SelectedItem as Record;
        }

       
    }
}
