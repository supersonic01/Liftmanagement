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
    /// Interaction logic for RecordingView.xaml
    /// </summary>
    public partial class RecordingView : UserControlView
    {
        public RecordingViewModel RcordingVM { get; set; } = new RecordingViewModel();

        protected override string ViewModelName => nameof(RcordingVM);
        protected override string SourceObjectStringName => nameof(RcordingVM.Record);

        public RecordingView()
        {
            InitializeComponent();
            this.Loaded += RecordingView_Loaded;
            
          
        }

        private void RecordingView_Loaded(object sender, RoutedEventArgs e)
        {
            SetData();
        }
        
        public void  SetData()
        { 
            
            SetComboBoxBindings();


            lblDate.Content = RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.DateVisual)) + ":";
            lblProcess.Content = RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.Process)) + ":";
            lblCostType.Content = RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.CostType)) + ":";
            lblMachineStoped.Content =
                RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.MachineStoped)) + ":";
            lblTimesensitive.Content =
                RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.Timesensitive)) + ":";
            lblInvoiceNumber.Content =
                RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.InvoiceNumber)) + ":";
            lblIssueLevel.Content =
                (RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.IssueLevel))).Replace("\n", " ") + ":";
            lblReportedFrom.Content = RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.ReportedFrom)) + ":";
            lblReason.Content = RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.Reason)) + ":";
            lblStorage.Content = RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.Storage)) + ":";
            lblNextStep.Content = RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.NextStep)) + ":";
            lblPersonResponsible.Content =
                RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.PersonResponsible)) + ":";
            lblReleaseFrom.Content = RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.ReleaseFrom)) + ":";
            lblCustomerInformed.Content =
                (RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.CustomerInformed))).Replace("\n", " ") +
                ":";
            lblCustomerPrefers.Content =
                (RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.CustomerPrefers))).Replace("\n", " ") +
                ":";
            lblOfferPrice.Content = RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.OfferPrice)) + ":";
            lblBillingAmountCorrect.Content =
                (RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.BillingAmountCorrect)))
                .Replace("\n", " ") + ":";
            lblExecutionCorrect.Content =
                (RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.ExecutionCorrect))).Replace("\n", " ") +
                ":";
            lblVerifiedOnSpot.Content =
                RcordingVM.Record.GetDisplayName<Recording>(nameof(RcordingVM.Record.VerifiedOnSpot)) + ":";



            //datePickerDate.SelectedDate = RcordingVM.Record.Date;
            //cbProcess.SelectedItem = RcordingVM.Record.Process;
            //cbCostType.SelectedItem = RcordingVM.Record.CostType;
            //cboxMachineStoped.IsChecked = RcordingVM.Record.MachineStoped;
            //datePickerTimesensitive.SelectedDate = RcordingVM.Record.Timesensitive;
            //txtInvoiceNumber.Text = RcordingVM.Record.InvoiceNumber;
            //cbIssueLevel.SelectedItem = RcordingVM.Record.IssueLevel;
            //cbReportedFrom.SelectedItem = RcordingVM.Record.ReportedFrom;
            //txtReason.Text = RcordingVM.Record.Reason;
            //cboxStorage.IsChecked = RcordingVM.Record.Storage;
            //txtNextStep.Text = RcordingVM.Record.NextStep;
            //cbPersonResponsible.SelectedItem = RcordingVM.Record.PersonResponsible;
            //cbReleaseFrom.SelectedItem = RcordingVM.Record.ReleaseFrom;
            //cboxCustomerInformed.IsChecked = RcordingVM.Record.CustomerInformed;
            //txtCustomerPrefers.Text = RcordingVM.Record.CustomerPrefers;
            //txtOfferPrice.Text = $"{RcordingVM.Record.OfferPrice:0,00}";
            //txtBillingAmountCorrect.Text = string.Format("{0:0,00}",RcordingVM.Record.BillingAmountCorrect);
            //cboxExecutionCorrect.IsChecked = RcordingVM.Record.ExecutionCorrect;
            //txtVerifiedOnSpot.Text = RcordingVM.Record.VerifiedOnSpot;

            //var binding = new Binding("RcordingVM.Record.BillingAmountCorrect")
            //{
            //   Source = this,
            //   StringFormat = "0,00"
            //};
            //txtNextStep.SetBinding(DatePicker.s.TextProperty, binding);

            BindingText(txtNextStep, nameof(RcordingVM.Record.NextStep));
            BindingText(txtVerifiedOnSpot, nameof(RcordingVM.Record.VerifiedOnSpot));
            BindingText(txtBillingAmountCorrect, nameof(RcordingVM.Record.BillingAmountCorrect));
            BindingText(txtOfferPrice, nameof(RcordingVM.Record.OfferPrice));
            BindingText(txtCustomerPrefers, nameof(RcordingVM.Record.CustomerPrefers));
            BindingText(txtReason, nameof(RcordingVM.Record.Reason));
            BindingText(txtInvoiceNumber, nameof(RcordingVM.Record.InvoiceNumber));

            BindingComboBoxSelectedItem(cbReleaseFrom, nameof(RcordingVM.Record.ReleaseFrom));
            BindingComboBoxSelectedItem(cbPersonResponsible, nameof(RcordingVM.Record.PersonResponsible));
            BindingComboBoxSelectedItem(cbReportedFrom, nameof(RcordingVM.Record.ReportedFrom));
            BindingComboBoxSelectedItem(cbIssueLevel, nameof(RcordingVM.Record.IssueLevel));
            BindingComboBoxSelectedItem(cbCostType, nameof(RcordingVM.Record.CostType));
            BindingComboBoxSelectedItem(cbProcess, nameof(RcordingVM.Record.Process));


            BindingCheckBox(cboxExecutionCorrect, nameof(RcordingVM.Record.ExecutionCorrect));
            BindingCheckBox(cboxCustomerInformed, nameof(RcordingVM.Record.CustomerInformed));
            BindingCheckBox(cboxStorage, nameof(RcordingVM.Record.Storage));
            BindingCheckBox(cboxMachineStoped, nameof(RcordingVM.Record.MachineStoped));

            BindingDatePicker(datePickerTimesensitive, nameof(RcordingVM.Record.Timesensitive));
            BindingDatePicker(datePickerDate, nameof(RcordingVM.Record.Date));
        }

        public void SetComboBoxBindings()
        {
            BindingComboBox(cbProcess, nameof(RcordingVM.Processes));
            BindingComboBox(cbCostType, nameof(RcordingVM.CostTypes));
            BindingComboBox(cbReportedFrom, nameof(RcordingVM.Reporters));
            BindingComboBox(cbPersonResponsible, nameof(RcordingVM.PersonsResponsible));
            BindingComboBox(cbReleaseFrom, nameof(RcordingVM.Releasers));
            BindingComboBox(cbIssueLevel, nameof(RcordingVM.IssueLevels));

        }

        private void datePickerDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void datePickerTimesensitive_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void btnProcessCompleted_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRequestForEditing_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
