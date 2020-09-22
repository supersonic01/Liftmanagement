using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Helper;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
  public class RecordingsViewModel
    {

        private ObservableCollection<Recording> recordings = new ObservableCollection<Recording>();

        public ObservableCollection<Recording> Recordings
        {
            get { return recordings; }
            set { recordings = value; }
        }
        

        public RecordingsViewModel()
        {
            recordings = TestData.GetRecordings();
        }

    }
}
