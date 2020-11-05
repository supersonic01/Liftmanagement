using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Liftmanagement.CollectionExtensions;
using Liftmanagement.Data;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
    public class RecordViewModel : ViewModel
    {
        private Task fillComboBoxTask;
        private List<string> processesDefault = new List<string> { "Rechnung AN", "TÜV Meldung", "Angebot AN", "AG Meldung", "Angebot Dritt", "RN Dritt", "Rechnung TÜV", "Gespräch AG", "Störungsmeldung AN" };
        private List<string> costTypesDefault = new List<string> { "Betriebskosten", "Reparatur/Instandhaltung", "Reparatur/Instandhaltung", "Betriebskostensenkung"};

        private List<string> reportersDefault = new List<string> { "TÜV", "Auftraggeber"};
        private List<string> releasersDefault = new List<string> { "Hesse", "Varga"};
        private List<string> personsResponsibleDefault = new List<string> { "Hesse", "Varga" };

        private Record recordSelected = new Record();

        public Record RecordSelected
        {
            get { return recordSelected; }
            set { SetField(ref recordSelected, value); }
        }

        public Record RecordSelectedLast { get; set; }

        private MachineInformation machineInformationSelected = new MachineInformation();

        public MachineInformation MachineInformationSelected
        {
            get { return machineInformationSelected; }
            set
            {
                SetField(ref machineInformationSelected, value);
            }
        }


        private ObservableCollection<Record> records = new ObservableCollection<Record>();

        public ObservableCollection<Record> Records
        {
            get { return records; }
            set
            {
                SetField(ref records, value);
                FillCombobox();
            }
        }


        private ObservableCollection<string> processes = new ObservableCollection<string>();

        public ObservableCollection<string> Processes
        {
            get
            {
                return processes;
            }
            set { SetField(ref processes, value); }
        }


        private ObservableCollection<string> costTypes;

        public ObservableCollection<string> CostTypes
        {
            get { return costTypes; }
            set { SetField(ref costTypes, value); }
        }


        private ObservableCollection<string> reporters;

        public ObservableCollection<string> Reporters
        {
            get { return reporters; }
            set { SetField(ref reporters, value); }
        }

        private ObservableCollection<string> personsResponsible;

        public ObservableCollection<string> PersonsResponsible
        {
            get { return personsResponsible; }
            set { SetField(ref personsResponsible, value); }
        }

        private ObservableCollection<string> releasers;

        public ObservableCollection<string> Releasers
        {
            get { return releasers; }
            set { SetField(ref releasers, value); }
        }

        private ObservableCollection<int> issueLevels;

        public ObservableCollection<int> IssueLevels
        {
            get { return issueLevels; }
            set { SetField(ref issueLevels, value); }
        }

        private ObservableCollection<RecordAgreementType> agreementType;

        public ObservableCollection<RecordAgreementType> AgreementType
        {
            get { return agreementType; }
            set { SetField(ref agreementType, value); }
        }




        public RecordViewModel()
        {
            // InitFillComboBoxTask();
        }

        private void InitFillComboBoxTask()
        {
            fillComboBoxTask = new Task(() =>
            {
                Processes = new ObservableCollection<string>(records.Select(c => c.Process).Distinct());
                costTypes = new ObservableCollection<string>(records.Select(c => c.CostType).Distinct());
                reporters = new ObservableCollection<string>(records.Select(c => c.ReportedFrom).Distinct());
                personsResponsible = new ObservableCollection<string>(records.Select(c => c.PersonResponsible).Distinct());
                releasers = new ObservableCollection<string>(records.Select(c => c.ReleaseFrom).Distinct());
                issueLevels = new ObservableCollection<int> { 1, 2, 3 };

            });
        }

        private void FillCombobox()
        {
            //Processes = GetComoBoxDefaultItemsDisstinct(processesDefault, () => records.Select(c => c.Process));
            //costTypes = GetComoBoxDefaultItemsDisstinct(costTypesDefault, () => records.Select(c => c.CostType));
            //reporters = GetComoBoxDefaultItemsDisstinct(reportersDefault, () => records.Select(c => c.ReportedFrom));
            //personsResponsible = GetComoBoxDefaultItemsDisstinct(personsResponsibleDefault, () => records.Select(c => c.PersonResponsible));
            //releasers = GetComoBoxDefaultItemsDisstinct(releasersDefault, () => records.Select(c => c.ReleaseFrom));
            //issueLevels = new ObservableCollection<int> { 1, 2, 3 };
            //AgreementType = GetAgreementType();

            Task.Factory.StartNew(() =>
            {
                Processes = GetComoBoxDefaultItemsDisstinct(processesDefault, () => records.Select(c => c.Process));
                costTypes = GetComoBoxDefaultItemsDisstinct(costTypesDefault, () => records.Select(c => c.CostType));
                reporters = GetComoBoxDefaultItemsDisstinct(reportersDefault, () => records.Select(c => c.ReportedFrom));
                personsResponsible = GetComoBoxDefaultItemsDisstinct(personsResponsibleDefault, () => records.Select(c => c.PersonResponsible));
                releasers = GetComoBoxDefaultItemsDisstinct(releasersDefault, () => records.Select(c => c.ReleaseFrom));
                issueLevels = new ObservableCollection<int> { 1, 2, 3 };
                AgreementType = GetAgreementType();
            });
        }

        private ObservableCollection<string> GetComoBoxDefaultItemsDisstinct(List<string> defaults, Func<IEnumerable<string>> dbList)
        {
            List<string> items = new List<string>();
            items.AddRange(defaults);
            items.AddRange(dbList());
            return new ObservableCollection<string>(items.Distinct().OrderBy(c=>c));
        }

      

        public SQLQueryResult<Record> Add()
        {
            RecordSelected.ReadOnly = false;
            if (RecordSelected.Id > 0)
            {
                return MySQLDataAccess.UpdateRecord(RecordSelected);
            }
            else
            {
                RecordSelected.CustomerId = machineInformationSelected.CustomerId;
                RecordSelected.LocationId = machineInformationSelected.LocationId;
                RecordSelected.MachineInformationId = machineInformationSelected.Id;
                return MySQLDataAccess.AddRecord(RecordSelected);
            }

        }

        public SQLQueryResult<Record> EditRecord()
        {
            var result = MySQLDataAccess.GetRecordForEdit(RecordSelected.Id);
            if (!result.IsReadOnly)
            {
                RecordSelected = result.DBRecords.FirstOrDefault() as Record;
            }

            return result;
        }

        public SQLQueryResult<Record> ForceEditing()
        {
            return MySQLDataAccess.ForceToEditRecord(RecordSelected.Id);
        }

        public void ReleaseEditing()
        {
            Task.Factory.StartNew(() =>
            {
                MySQLDataAccess.ReleaseEditingRecord(RecordSelected.Id);
            });
        }

        public SQLQueryResult<Record> MarkForDeleteRecord()
        {
            return MySQLDataAccess.MarkForDeleteRecord(RecordSelected);
        }

        private ObservableCollection<RecordAgreementType> GetAgreementType()
        {
            if (machineInformationSelected == null || machineInformationSelected.Id < 0)
                return new ObservableCollection<RecordAgreementType>();

            var maintenanceAgreements = MySQLDataAccess.GetMaintenanceAgreements(machineInformationSelected.Id).OrderByDescending(c => c.AgreementDate);

            List<RecordAgreementType> agreements = new List<RecordAgreementType>();

            foreach (var maintenanceAgreement in maintenanceAgreements)
            {
                agreements.Add(new RecordAgreementType
                {
                    AgreementType = Helper.Helper.TTypeMangement.MaintenanceAgreement,
                    MaintenanceAgreement = maintenanceAgreement
                });
            }

            return new ObservableCollection<RecordAgreementType>(agreements);
        }

    }


}
