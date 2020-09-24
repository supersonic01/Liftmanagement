using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
    public class RecordingViewModel : ViewModel
    {
        private Task fillComboBoxTask;
        private Recording record = new Recording();

        public Recording Record
        {
            get { return record; }
            set { SetField(ref record, value); }
        }


        private ObservableCollection<Recording> recordings = new ObservableCollection<Recording>();

        public ObservableCollection<Recording> Recordings
        {
            get { return recordings; }
            set
            {
                SetField(ref recordings, value); 
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

        

        public RecordingViewModel()
        {
            InitFillComboBoxTask();
        }

        private void InitFillComboBoxTask()
        {
            fillComboBoxTask = new Task(() =>
            {
                Processes = new ObservableCollection<string>(recordings.Select(c => c.Process).Distinct());
                costTypes = new ObservableCollection<string>(recordings.Select(c => c.CostType).Distinct());
                reporters = new ObservableCollection<string>(recordings.Select(c => c.ReportedFrom).Distinct());
                personsResponsible = new ObservableCollection<string>(recordings.Select(c => c.PersonResponsible).Distinct());
                releasers = new ObservableCollection<string>(recordings.Select(c => c.ReleaseFrom).Distinct());
                issueLevels = new ObservableCollection<int> {1, 2, 3};

            });
        }

        private void FillCombobox()
        {
            fillComboBoxTask.Start();
           
        }
    }


}
