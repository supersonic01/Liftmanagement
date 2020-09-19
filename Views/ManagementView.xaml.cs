using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using System;
using System.CodeDom;
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
    /// Interaction logic for ManagementView.xaml
    /// </summary>
    public partial class ManagementView : UserControl
    {      
        private ManagementViewModel managementVM= new ManagementViewModel();

        public ManagementViewModel ManagementVM
        {
            get { return managementVM; }
            set { managementVM = value; }
        }


        public ManagementView()
        {
            InitializeComponent();

            lblCustomerHeader.Content = "Rechnungsadresse:";
            lblLocationHeader.Content = "Standort:";
            lblMaintenanceIntervalHeader.Content = "Wartungsinterval:\n4x / vierteljährlich";
            
            BindingControll(cbCustomers, nameof(ManagementVM.Customers));
            BindingControll(dgOthers, nameof(ManagementVM.OtherInformations));

            var location = ManagementVM.Locations.FirstOrDefault();
            var customer = ManagementVM.Customers.FirstOrDefault();
            var maintenanceAgreement = ManagementVM.MaintenanceAgreements.FirstOrDefault();
            var machineInfo = ManagementVM.MachineInformations.FirstOrDefault();

            SetLocationData(location);
            SetCustomerData(customer);
            SetMaintenanceAgreementData(maintenanceAgreement);
            SetEmergencyAgreementData(maintenanceAgreement);
            SetMachineInformationData(machineInfo);

            dgContactPersons.ItemsSource = customer.Administrator.ContactPersons;
            dgContactPersons.Tag = ManagementVM.NotVisibleColumns;

         
            dgOthers.PreviewKeyDown += DgOthers_PreviewKeyDown;

            //TODO dgOthers cell  make vertical scrollabl
        }

        private void BindingControll(ItemsControl control,  string source)
        {
            Binding binding = new Binding("ManagementVM."+source)
            {
                Source = this
            };

            control.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        private void DgOthers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (dgOthers.SelectedIndex == dgOthers.Items.Count - 1)
                {
                    ManagementVM.OtherInformations.Add(new OtherInformation(""));
                    e.Handled = true;
                    //TODO dgOthers cell Textbox courser is set proper by new cell
                }

                dgOthers.CurrentCell = new DataGridCellInfo(
                    dgOthers.Items[dgOthers.SelectedIndex + 1], dgOthers.Columns[0]);
                dgOthers.BeginEdit();

              
            }
        }
     
        private void SetMaintenanceAgreementData(MaintenanceAgreement maintenanceAgreement)
        {
            lblMaintenanceDuration.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.Duration))+":";
            lblMaintenanceDate.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.AgreementDate)) + ":";
            lblMaintenanceNoticeOfPeriod.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.NoticeOfPeriod)) + ":";
            lblMaintenanceCanBeCancelled.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.CanBeCancelled)) + ":";

            lblMaintenanceDurationValue.Content = maintenanceAgreement.Duration.ToString("dd.MM.yyyy");
            lblMaintenanceDateValue.Content = maintenanceAgreement.AgreementDate.ToString("dd.MM.yyyy");
            lblMaintenanceNoticeOfPeriodValue.Content = maintenanceAgreement.NoticeOfPeriod;
            lblMaintenanceCanBeCancelledValue.Content = maintenanceAgreement.CanBeCancelled;          
        }

        //TODO
        private void SetEmergencyAgreementData(MaintenanceAgreement maintenanceAgreement)
        {
            lblEmergencyDuration.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.Duration)) + ":";
            lblEmergencyDate.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.AgreementDate)) + ":";
            lblEmergencyNoticeOfPeriod.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.NoticeOfPeriod)) + ":";
            lblEmergencyCanBeCancelled.Content = maintenanceAgreement.GetDisplayName<MaintenanceAgreement>(nameof(maintenanceAgreement.CanBeCancelled)) + ":";

            lblEmergencyDurationValue.Content = maintenanceAgreement.Duration.ToString("dd.MM.yyyy");
            lblEmergencyDateValue.Content = maintenanceAgreement.AgreementDate.ToString("dd.MM.yyyy");
            lblEmergencyNoticeOfPeriodValue.Content = maintenanceAgreement.NoticeOfPeriod;
            lblEmergencyCanBeCancelledValue.Content = maintenanceAgreement.CanBeCancelled;
        }

        private void SetMachineInformationData(MachineInformation machineInformation)
        {
            lblMachineInfoName.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Name)) + ":";
            lblMachineInfoSerialNumber.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.SerialNumber)) + ":";
            lblMachineInfoHoldingPositions.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.HoldingPositions)) + ":";
            lblMachineInfoEntrances.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Entrances)) + ":";
            lblMachineInfoYearOfConstruction.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.YearOfConstruction)) + ":";
            lblMachineInfoPayload.Content = machineInformation.GetDisplayName<MachineInformation>(nameof(machineInformation.Payload)) + ":";

            lblMachineInfoNameValue.Content = machineInformation.Name;
            lblMachineInfoSerialNumberValue.Content = machineInformation.SerialNumber;
            lblMachineInfoHoldingPositionsValue.Content = machineInformation.HoldingPositions;
            lblMachineInfoEntrancesValue.Content = machineInformation.Entrances;
            lblMachineInfoYearOfConstructionValue.Content = machineInformation.YearOfConstruction.ToString("dd.MM.yyyy"); 
            lblMachineInfoPayloadValue.Content = machineInformation.Payload;
        }
              
        private void SetLocationData(Location location)
        {
            lblAddressLocation.Content = location.Address;
            lblPostcodeCityLocation.Content = location.GetPostcodeCity();
        }
        private void SetCustomerData(Customer customer)
        {
            lblCompanyName.Content = customer.CompanyName;
            lblAdministratorCompany.Content = customer.Administrator.Name;
            lblAddress.Content = customer.Address;
            lblPostcodeCity.Content = customer.GetPostcodeCity();
        }

        }
    
}
