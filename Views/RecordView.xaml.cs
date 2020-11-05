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
using Liftmanagement.Converters;
using Liftmanagement.Models;
using Liftmanagement.ViewModels;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for RecordView.xaml
    /// </summary>
    public partial class RecordView : GoogleDriveDialogueView
    {
        public RecordViewModel RecordVM { get; set; } = new RecordViewModel();

        public delegate Record RecordUpdatedDelegate(long recordId);

        public RecordUpdatedDelegate RecordUpdated { get; set; }


        public RecordView()
        {
            InitializeComponent();
            // this.Loaded += RecordView_Loaded;
            AssigneValuesToControl();

        }

        private void RecordView_Loaded(object sender, RoutedEventArgs e)
        {
            AssigneValuesToControl();
        }

        public void EnableContoles(bool enable)
        {
            base.EnableContoles(enable);
        }

        public void AssigneValuesToControl()
        {
            SetComboBoxBindings();

            lblDate.Content = RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.DateVisual)) + ":";
            lblProcess.Content = RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.Process)) + ":";
            lblCostType.Content = RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.CostType)) + ":";
            lblMachineStoped.Content =
                RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.MachineStoped)) + ":";
            lblTimesensitive.Content =
                RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.Timesensitive)) + ":";
            lblInvoiceNumber.Content =
                RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.InvoiceNumber)) + ":";
            lblIssueLevel.Content =
                (RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.IssueLevel))).Replace("\n", " ") + ":";
            lblReportedFrom.Content = RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.ReportedFrom)) + ":";
            lblReason.Content = RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.Reason)) + ":";
            lblStorage.Content = RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.Storage)) + ":";
            lblNextStep.Content = RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.NextStep)) + ":";
            lblPersonResponsible.Content =
                RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.PersonResponsible)) + ":";
            lblReleaseFrom.Content = RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.ReleaseFrom)) + ":";
            lblCustomerInformed.Content =
                (RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.CustomerInformed))).Replace("\n", " ") +
                ":";
            lblCustomerPrefers.Content =
                (RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.CustomerPrefers))).Replace("\n", " ") +
                ":";
            lblOfferPrice.Content = RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.OfferPrice)) + ":";
            lblBillingAmountCorrect.Content =
                (RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.BillingAmountCorrect)))
                .Replace("\n", " ") + ":";
            lblExecutionCorrect.Content =
                (RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.ExecutionCorrect))).Replace("\n", " ") +
                ":";
            lblVerifiedOnSpot.Content =
                RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.VerifiedOnSpot)) + ":";

            lblAgreementType.Content =
                RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.AgreementType)) + ":";

            lblGoogleDriveLink.Content = RecordVM.RecordSelected.GetDisplayName<Record>(nameof(RecordVM.RecordSelected.GoogleDriveFolderName)) + ":";
            

            BindingText(txtNextStep, ()=>RecordVM.RecordSelected.NextStep);
            BindingText(txtVerifiedOnSpot, ()=>RecordVM.RecordSelected.VerifiedOnSpot);
            BindingText(txtBillingAmountCorrect, ()=>RecordVM.RecordSelected.BillingAmountCorrect);
            BindingText(txtOfferPrice, ()=>RecordVM.RecordSelected.OfferPrice);
            BindingText(txtCustomerPrefers, ()=>RecordVM.RecordSelected.CustomerPrefers);
            BindingText(txtReason, ()=>RecordVM.RecordSelected.Reason);
            BindingText(txtInvoiceNumber, ()=>RecordVM.RecordSelected.InvoiceNumber);

            BindingHyperlink(hyperlinkGoogleDrive, () => RecordVM.RecordSelected.GoogleDriveLink);
            BindingTextBlock(txtGoogleDriveFolderName, () => RecordVM.RecordSelected.GoogleDriveFolderName);

            BindingComboBoxSelectedItem(cbReleaseFrom, ()=>RecordVM.RecordSelected.ReleaseFrom);
            BindingComboBoxSelectedItem(cbPersonResponsible, ()=>RecordVM.RecordSelected.PersonResponsible);
            BindingComboBoxSelectedItem(cbReportedFrom, ()=>RecordVM.RecordSelected.ReportedFrom);
            BindingComboBoxSelectedItem(cbIssueLevel, ()=>RecordVM.RecordSelected.IssueLevel);
            BindingComboBoxSelectedItem(cbCostType, ()=>RecordVM.RecordSelected.CostType);
            BindingComboBoxSelectedItem(cbProcess, ()=>RecordVM.RecordSelected.Process);
            BindingComboBoxSelectedItem(cbAgreementType, () => RecordVM.RecordSelected.Agreement);
            
            BindingComboBoxText(cbReleaseFrom, () => RecordVM.RecordSelected.ReleaseFrom);
            BindingComboBoxText(cbPersonResponsible, () => RecordVM.RecordSelected.PersonResponsible);
            BindingComboBoxText(cbReportedFrom, () => RecordVM.RecordSelected.ReportedFrom);
            BindingComboBoxText(cbCostType, () => RecordVM.RecordSelected.CostType);
            BindingComboBoxText(cbProcess, () => RecordVM.RecordSelected.Process);


            BindingCheckBox(cboxExecutionCorrect, ()=>RecordVM.RecordSelected.ExecutionCorrect);
            BindingCheckBox(cboxCustomerInformed, ()=>RecordVM.RecordSelected.CustomerInformed);
            BindingCheckBox(cboxStorage, ()=>RecordVM.RecordSelected.Storage);
            BindingCheckBox(cboxMachineStoped, ()=>RecordVM.RecordSelected.MachineStoped);

            BindingDatePicker(datePickerTimesensitive, ()=>RecordVM.RecordSelected.Timesensitive);
            BindingDatePicker(datePickerDate, ()=>RecordVM.RecordSelected.Date);



            var binding = new Binding(GetPropertyPath(() => RecordVM.RecordSelected.ProcessCompleted))
            {
                Source = this,
            };

            binding.Mode = BindingMode.TwoWay;

            binding.Converter = new BooleanToImageConverter();

            imageBtnProcessCompleted.SetBinding(Image.SourceProperty, binding);

        }

        public void SetComboBoxBindings()
        {
            BindingComboBoxBindingModeOneWay(cbProcess, ()=>RecordVM.Processes);
            BindingComboBoxBindingModeOneWay(cbCostType, ()=>RecordVM.CostTypes);
            BindingComboBoxBindingModeOneWay(cbReportedFrom, ()=>RecordVM.Reporters);
            BindingComboBoxBindingModeOneWay(cbPersonResponsible, ()=>RecordVM.PersonsResponsible);
            BindingComboBoxBindingModeOneWay(cbReleaseFrom, ()=>RecordVM.Releasers);
            BindingComboBoxBindingModeOneWay(cbIssueLevel, ()=>RecordVM.IssueLevels);
            BindingComboBoxBindingModeOneWay(cbAgreementType, () => RecordVM.AgreementType);


            //BindingComboBox(cbProcess, () => RcordingVM.Processes);
            //BindingComboBox(cbCostType, () => RcordingVM.CostTypes);
            //BindingComboBox(cbReportedFrom, () => RcordingVM.Reporters);
            //BindingComboBox(cbPersonResponsible, () => RcordingVM.PersonsResponsible);
            //BindingComboBox(cbReleaseFrom, () => RcordingVM.Releasers);
            //BindingComboBox(cbIssueLevel, () => RcordingVM.IssueLevels);

        }

        protected override void BtnSaveGoogleDrive_Click(object sender, RoutedEventArgs e)
        {
            var node = googlDriveTree.GetSelectedNode();

            RecordVM.RecordSelected.GoogleDriveLink = node.WebLink;
            RecordVM.RecordSelected.GoogleDriveFolderName = node.Name;

            windowGoogleDriveTree.Hide();
        }

        private void btnGoogleDriveTree_Click(object sender, RoutedEventArgs e)
        {
            if (RecordVM.RecordSelected != null)
            {
                windowGoogleDriveTree.Title = windowGoogleDriveTree.Title + "  " + RecordVM.RecordSelected.GetFullName();
            }

            windowGoogleDriveTree.ShowDialog();
        }

        private void ForceValidation()
        {
            //txtName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            //txtYearOfConstruction.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            //txtSerialNumber.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dd = RecordVM.RecordSelected.Timesensitive == null
                    ? ""
                    : RecordVM.RecordSelected.Timesensitive.Value.ToString("yyyy-MM-dd");


                ForceValidation();
                bool isValid = true;
                List<string> validationMsg = new List<string>();


                if (string.IsNullOrWhiteSpace(RecordVM.RecordSelected.Process))
                {
                    validationMsg.Add("Vorgang darf nicht leer sein.");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(RecordVM.RecordSelected.CostType))
                {
                    validationMsg.Add("Kostenart darf nicht leer sein.");
                    isValid = false;
                }
                
                if (!isValid)
                {
                    new NotificationWindow("Fehler!", null, validationMsg, NotificationWindow.MessageType.Error).Show();
                    return;
                }

                var result = RecordVM.Add();
                if (result.Records > 0)
                {
                    Record record = RecordUpdated(result.Id);
                    RecordVM.RecordSelected = record;
                  
                    

                    var titel = string.Format("Vorgang : {0}", record.GetFullName());
                    var msg = "Vorgangsdaten wurden gespeichert.";
                    new NotificationWindow(titel, msg,null, NotificationWindow.MessageType.Error).Show();

                    EnableContoles(false);
                }
                else
                {
                    var msg = "Vorgangsdaten konnten nicht gespeichert werden.";
                    new NotificationWindow("Fehler!", msg).Show();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                var msg = "Vorgangsdaten konnten nicht gespeichert werden.";
                new NotificationWindow("Fehler!", exception.ToString(),null,NotificationWindow.MessageType.Error).Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RecordVM.RecordSelected = new Record();
            EnableContoles(true);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            RecordVM.RecordSelectedLast = RecordVM.RecordSelected;
            var result = RecordVM.EditRecord();
            if (result.IsReadOnly)
            {
                AskForceToEdit(result.CurrentlyUsedBy, () =>
                {
                    RecordVM.ForceEditing();
                    EnableContoles(true);
                });
            }
            else
            {
                EnableContoles(true);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (RecordVM.RecordSelected.ReadOnly)
            {
                RecordVM.ReleaseEditing();
            }

            RecordVM.RecordSelected = RecordVM.RecordSelectedLast;
            EnableContoles(false);

        }
        
        private void btnProcessCompleted_Click(object sender, RoutedEventArgs e)
        {
            RecordVM.RecordSelected.ProcessCompleted = !RecordVM.RecordSelected.ProcessCompleted;
            RecordVM.RecordSelected.ProcessCompletedPerson= Helper.Helper.GetPersonName();

            //TODO boolButton image converter
        }
    }
}
