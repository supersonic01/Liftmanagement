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

namespace Liftmanagement.ViewModels
{
    public class MaintenanceAgreementViewModel : ViewModel
    {


        private List<MaintenanceAgreement> maintenanceAgreements = new List<MaintenanceAgreement>();

        public List<MaintenanceAgreement> MaintenanceAgreements
        {
            get { return maintenanceAgreements; }
            set { SetField(ref maintenanceAgreements, value); }
        }

        private ObservableCollection<string> terminationUnits = new ObservableCollection<string>();

        public ObservableCollection<string> TerminationUnits
        {
            get { return terminationUnits; }
            set { SetField(ref terminationUnits, value); }
        }

        private ObservableCollection<string> maintenanceTypes = new ObservableCollection<string>();

        public ObservableCollection<string> MaintenanceTypes
        {
            get { return maintenanceTypes; }
            set { SetField(ref maintenanceTypes, value); }
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

        private MaintenanceAgreement maintenanceAgreementSelected = new MaintenanceAgreement();

        public MaintenanceAgreement MaintenanceAgreementSelected
        {
            get { return maintenanceAgreementSelected; }
            set
            {
                SetField(ref maintenanceAgreementSelected, value);
            }
        }

        public MaintenanceAgreement MaintenanceAgreementSelectedLast { get; set; }

        public SQLQueryResult<MaintenanceAgreement> Add(MasterDataInfoViewModel masterDataInfoVM)
        {
            maintenanceAgreementSelected.ReadOnly = false;
            SQLQueryResult<MaintenanceAgreement> result = null;
            if (MaintenanceAgreementSelected.Id > 0)
            {
                result = MySQLDataAccess.UpdateMaintenanceAgreement(MaintenanceAgreementSelected);
            }
            else
            {
                if (MaintenanceAgreementSelected.NotificationTime > 0)
                    MaintenanceAgreementSelected.GoogleCalendarEventId = AddGooleEvent(masterDataInfoVM);

                MaintenanceAgreementSelected.CustomerId = masterDataInfoVM.MachineInformationSelected.CustomerId;
                MaintenanceAgreementSelected.LocationId = masterDataInfoVM.MachineInformationSelected.Id;
                MaintenanceAgreementSelected.MachineInformationId = masterDataInfoVM.MachineInformationSelected.Id;
                result = MySQLDataAccess.AddMaintenanceAgreement(MaintenanceAgreementSelected);
            }

            LoadComboboxes();

            return result;
        }

        private string AddGooleEvent(MasterDataInfoViewModel masterDataInfoVM)
        {
            TimeSpan ts = new TimeSpan(9, 00, 0);
            DateTime start = DateTime.Now ;
           

            switch (MaintenanceAgreementSelected.NotificationUnit)
            {
                case Helper.Helper.NotificationUnitType.days:
                    start = MaintenanceAgreementSelected.Duration.AddDays(MaintenanceAgreementSelected.NotificationTime * -1);
                    break;
                case Helper.Helper.NotificationUnitType.weeks:
                    start = MaintenanceAgreementSelected.Duration.AddDays(MaintenanceAgreementSelected.NotificationTime * -7);
                    break;
                case Helper.Helper.NotificationUnitType.months:
                    start = MaintenanceAgreementSelected.Duration.AddMonths(MaintenanceAgreementSelected.NotificationTime * -1);
                    break;
            }

            string summary = "Kündigung Wartungsvertrag, " + masterDataInfoVM.CustomerSelected.CompanyName;
            StringBuilder description = new StringBuilder();
            description.AppendLine("Kunde: " + masterDataInfoVM.CustomerSelected.GetFullName());
            description.AppendLine("Standort: " + masterDataInfoVM.LocationSelected.GetFullName());
            description.AppendLine("Anlage: " + masterDataInfoVM.MachineInformationSelected.GetFullName());
            description.AppendLine("Wartungsvertrag: " + MaintenanceAgreementSelected.GetFullName());

            start = start.Date + ts;

            string edi = new CalendarQuickstart().AddEvent(start, start.AddMinutes(30), summary, description.ToString());

            return edi;
        }

        public SQLQueryResult<MaintenanceAgreement> EditMaintenanceAgreement()
        {
            var result = MySQLDataAccess.GetMaintenanceAgreementForEdit(MaintenanceAgreementSelected.Id);
            if (!result.IsReadOnly)
            {
                MaintenanceAgreementSelected = result.DBRecords.FirstOrDefault() as MaintenanceAgreement;
            }

            return result;
        }

        public SQLQueryResult<MaintenanceAgreement> ForceEditing()
        {
            return MySQLDataAccess.ForceToEditMaintenanceAgreement(MaintenanceAgreementSelected.Id);
        }

        public void ReleaseEditing()
        {
            Task.Factory.StartNew(() =>
            {
                MySQLDataAccess.ReleaseEditingMaintenanceAgreement(MaintenanceAgreementSelected.Id);
            });
        }

        public SQLQueryResult<MaintenanceAgreement> MarkForDeleteMaintenanceAgreement()
        {
            return MySQLDataAccess.MarkForDeleteMaintenanceAgreement(MaintenanceAgreementSelected);
        }

        public void RefreshByMachineInformatio(long id)
        {
            MaintenanceAgreements = MySQLDataAccess.GetMaintenanceAgreementsByMachineInformation(id);
        }
        public void Refresh(long id)
        {
            MaintenanceAgreements = MySQLDataAccess.GetMaintenanceAgreements();
        }

        public void LoadComboboxes()
        {
            SetTerminationUnits();
            SetArreementCancelledBy();
            SetMaintenanceType();
        }
        private void SetTerminationUnits()
        {
            var terminations = maintenanceAgreements.Select(c => c.CanBeCancelled).ToList();
            terminations.Insert(0, Properties.Resources.yearly);

            TerminationUnits = new ObservableCollection<string>(terminations.Distinct());
        }
        private void SetArreementCancelledBy()
        {
            var person = maintenanceAgreements.Select(c => c.AgreementCancelledBy).ToList();
            person.Insert(0, "Kunde");

            ArreementCancelledBy = new ObservableCollection<string>(person.Distinct());
        }

        private void SetMaintenanceType()
        {
            var maintenance = maintenanceAgreements.Select(c => c.MaintenanceType).ToList();
            maintenance.Insert(0, Properties.Resources.fullService);
            maintenance.Insert(0, Properties.Resources.systemService);

            MaintenanceTypes = new ObservableCollection<string>(maintenance.Distinct());
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
