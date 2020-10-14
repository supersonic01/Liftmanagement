﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Helper;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
    public class RecordingsViewModel : ViewModel
    {

        private ObservableCollection<Recording> recordings = new ObservableCollection<Recording>();

        public ObservableCollection<Recording> Recordings
        {
            get { return recordings; }
            set { SetField(ref recordings, value); }
        }


        //private ObservableCollection<string> processes;

        //public ObservableCollection<string> Processes
        //{
        //    get { return processes; }
        //    set { SetField(ref processes, value); }
        //}



        //private ObservableCollection<string> costTypes;

        //public ObservableCollection<string> CostTypes
        //{
        //    get { return costTypes; }
        //    set { SetField(ref costTypes, value); }
        //}


        //private ObservableCollection<string> reporters;

        //public ObservableCollection<string> Reporters
        //{
        //    get { return reporters; }
        //    set { SetField(ref reporters, value); }
        //}

        //private ObservableCollection<string> personsResponsible;

        //public ObservableCollection<string> PersonsResponsible
        //{
        //    get { return personsResponsible; }
        //    set { SetField(ref personsResponsible, value); }
        //}

        //private ObservableCollection<string> releasers;

        //public ObservableCollection<string> Releasers
        //{
        //    get { return releasers; }
        //    set { SetField(ref releasers, value); }
        //}



        public RecordingsViewModel()
        {
            recordings = TestData.GetRecordings();

           
        }

    }
}