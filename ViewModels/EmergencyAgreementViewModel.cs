using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.CollectionExtensions;
using Liftmanagement.Data;
using Liftmanagement.Views;

namespace Liftmanagement.ViewModels
{
    public class EmergencyAgreementViewModel : ViewModel
    {


        private List<EmergencyAgreement> emergencyAgreements = new List<EmergencyAgreement>();

        public List<EmergencyAgreement> EmergencyAgreements
        {
            get { return emergencyAgreements; }
            set { SetField(ref emergencyAgreements, value); }
        }

        private ObservableCollection<string> terminationUnits = new ObservableCollection<string>();

        public ObservableCollection<string> TerminationUnits
        {
            get { return terminationUnits; }
            set { SetField(ref terminationUnits, value); }
        }

        private ObservableCollection<string> emergencyTypes = new ObservableCollection<string>();

        public ObservableCollection<string> EmergencyTypes
        {
            get { return emergencyTypes; }
            set { SetField(ref emergencyTypes, value); }
        }

        private Dictionary<Helper.Helper.NotificationUnitType, string> notificationUnits = null;

        public Dictionary<Helper.Helper.NotificationUnitType, string> NotificationUnits
        {
            get
            {
                if (notificationUnits == null)
                {
                    notificationUnits = GetNotificationUnits();
                }
                return notificationUnits;
            }
        }

        private ObservableCollection<string> arreementCancelledBy = new ObservableCollection<string>();

        public ObservableCollection<string> ArreementCancelledBy
        {
            get { return arreementCancelledBy; }
            set { SetField(ref arreementCancelledBy, value); }
        }

        private EmergencyAgreement emergencyAgreementSelected = new EmergencyAgreement();

        public EmergencyAgreement EmergencyAgreementSelected
        {
            get { return emergencyAgreementSelected; }
            set
            {
                if (RefreshEmergencyAgreementContent != null && value != null &&
                    EqualityComparer<EmergencyAgreement>.Default.Equals(emergencyAgreementSelected, value) == false)
                {
                    Task.Factory.StartNew(() => { RefreshEmergencyAgreementContent(value.Id); });
                }

                SetField(ref emergencyAgreementSelected, value);
            }
        }

        public EmergencyAgreement EmergencyAgreementSelectedLast { get; set; }


        public delegate void RefreshEmergencyAgreementContentDelegate(long emergencyAgreementId);

        public RefreshEmergencyAgreementContentDelegate RefreshEmergencyAgreementContent { get; set; }



        public SQLQueryResult<EmergencyAgreement> Add(MasterDataInfoViewModel masterDataInfoVM)
        {
            emergencyAgreementSelected.ReadOnly = false;
            SQLQueryResult<EmergencyAgreement> result = null;

            if (EmergencyAgreementSelected.Id > 0)
            {
                //TODO update Kaleder event missing
               result = MySQLDataAccess.UpdateEmergencyAgreement(EmergencyAgreementSelected);
            }
            else
            {
                if (EmergencyAgreementSelected.NotificationTime > 0)
                    EmergencyAgreementSelected.GoogleCalendarEventId = AddGooleEvent(masterDataInfoVM);

                EmergencyAgreementSelected.CustomerId = masterDataInfoVM.MachineInformationSelected.CustomerId;
                EmergencyAgreementSelected.LocationId = masterDataInfoVM.MachineInformationSelected.Id;
                EmergencyAgreementSelected.MachineInformationId = masterDataInfoVM.MachineInformationSelected.Id;
                result = MySQLDataAccess.AddEmergencyAgreement(EmergencyAgreementSelected);
            }

            LoadComboboxes();

            return result;
        }

        private string AddGooleEvent(MasterDataInfoViewModel masterDataInfoVM)
        {
            TimeSpan ts = new TimeSpan(9, 00, 0);
            DateTime start = DateTime.Now;


            switch (EmergencyAgreementSelected.NotificationUnit)
            {
                case Helper.Helper.NotificationUnitType.days:
                    start = EmergencyAgreementSelected.Duration.AddDays(EmergencyAgreementSelected.NotificationTime * -1);
                    break;
                case Helper.Helper.NotificationUnitType.weeks:
                    start = EmergencyAgreementSelected.Duration.AddDays(EmergencyAgreementSelected.NotificationTime * -7);
                    break;
                case Helper.Helper.NotificationUnitType.months:
                    start = EmergencyAgreementSelected.Duration.AddMonths(EmergencyAgreementSelected.NotificationTime * -1);
                    break;
            }

            string summary = "Kündigung Notrufvertrag, " + masterDataInfoVM.CustomerSelected.CompanyName;
            StringBuilder description = new StringBuilder();
            description.AppendLine("Kunde: " + masterDataInfoVM.CustomerSelected.GetFullName());
            description.AppendLine("Standort: " + masterDataInfoVM.LocationSelected.GetFullName());
            description.AppendLine("Anlage: " + masterDataInfoVM.MachineInformationSelected.GetFullName());
            description.AppendLine("Wartungsvertrag: " + EmergencyAgreementSelected.GetFullName());

            start = start.Date + ts;

            string edi = new CalendarQuickstart().AddEvent(start, start.AddMinutes(30), summary, description.ToString());

            return edi;
        }

        public SQLQueryResult<EmergencyAgreement> EditEmergencyAgreement()
        {
            var result = MySQLDataAccess.GetEmergencyAgreementForEdit(EmergencyAgreementSelected.Id);
            if (!result.IsReadOnly)
            {
                EmergencyAgreementSelected = result.DBRecords.FirstOrDefault() as EmergencyAgreement;
            }

            return result;
        }

        public SQLQueryResult<EmergencyAgreement> ForceEditing()
        {
            return MySQLDataAccess.ForceToEditEmergencyAgreement(EmergencyAgreementSelected.Id);
        }

        public void ReleaseEditing()
        {
            Task.Factory.StartNew(() =>
            {
                MySQLDataAccess.ReleaseEditingEmergencyAgreement(EmergencyAgreementSelected.Id);
            });
        }

        public SQLQueryResult<EmergencyAgreement> MarkForDeleteEmergencyAgreement()
        {
            return MySQLDataAccess.MarkForDeleteEmergencyAgreement(EmergencyAgreementSelected);
        }

        public void RefreshByMachineInformatio(long id)
        {
            EmergencyAgreements = MySQLDataAccess.GetEmergencyAgreementsByMachineInformation(id);
        }
        public void Refresh(long id)
        {
            EmergencyAgreements = MySQLDataAccess.GetEmergencyAgreements();
        }

        public void LoadComboboxes()
        {
            SetTerminationUnits();
            SetArreementCancelledBy();
            SetMaintenanceType();
        }
        private void SetTerminationUnits()
        {
            var terminations = emergencyAgreements.Select(c => c.CanBeCancelled).ToList();
            terminations.Insert(0, Properties.Resources.yearly);

            TerminationUnits = new ObservableCollection<string>(terminations.Distinct());
        }
        private void SetArreementCancelledBy()
        {
            var person = emergencyAgreements.Select(c => c.AgreementCancelledBy).ToList();
            person.Insert(0, "Kunde");

            ArreementCancelledBy = new ObservableCollection<string>(person.Distinct());
        }

        private void SetMaintenanceType()
        {
            var maintenance = emergencyAgreements.Select(c => c.EmergencyType).ToList();
            maintenance.Insert(0, Properties.Resources.withPersonsRescue);
            maintenance.Insert(0, Properties.Resources.withoutPersonsRescue);

            EmergencyTypes = new ObservableCollection<string>(maintenance.Distinct());
        }

        private Dictionary<Helper.Helper.NotificationUnitType, string> GetNotificationUnits()
        {
            Dictionary<Helper.Helper.NotificationUnitType, string> notificationUnits = new Dictionary<Helper.Helper.NotificationUnitType, string>();

            notificationUnits.Add(Helper.Helper.NotificationUnitType.days, Properties.Resources.ResourceManager.GetString(Helper.Helper.NotificationUnitType.days.ToString()));
            notificationUnits.Add(Helper.Helper.NotificationUnitType.weeks, Properties.Resources.ResourceManager.GetString(Helper.Helper.NotificationUnitType.weeks.ToString()));
            notificationUnits.Add(Helper.Helper.NotificationUnitType.months, Properties.Resources.ResourceManager.GetString(Helper.Helper.NotificationUnitType.months.ToString()));

            return notificationUnits;
        }


    }


}
