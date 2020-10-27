using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Data;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
    public class RecordViewModel : ViewModel
    {
        private Task fillComboBoxTask;
        private Record recordSelected = new Record();

        public Record RecordSelected
        {
            get { return recordSelected; }
            set { SetField(ref recordSelected, value); }
        }

        public Record RecordSelectedLast { get; set; }

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

        

        public RecordViewModel()
        {
            InitFillComboBoxTask();
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
                issueLevels = new ObservableCollection<int> {1, 2, 3};

            });
        }

        private void FillCombobox()
        {
            fillComboBoxTask.Start();
        }

        public SQLQueryResult<Record> Add(MachineInformation machineInformation)
        {
            RecordSelected.ReadOnly = false;
            if (RecordSelected.Id > 0)
            {
                return MySQLDataAccess.UpdateRecord(RecordSelected);
            }
            else
            {
                RecordSelected.CustomerId = machineInformation.CustomerId;
                RecordSelected.LocationId = machineInformation.Id;
                RecordSelected.LocationId = machineInformation.Id;
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

        public void RefreshByLocation(long id)
        {
            Records = MySQLDataAccess.GetRecordsByLocation(id);
        }
    }


}
