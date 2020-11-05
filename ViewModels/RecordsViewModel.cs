using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Data;
using Liftmanagement.Helper;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
    public class RecordsViewModel : ViewModel
    {

        private ObservableCollection<Record> records = new ObservableCollection<Record>();

        public ObservableCollection<Record> Records
        {
            get { return records; }
            set { SetField(ref records, value); }
        }



        public void Refresh(MachineInformation machineInformation)
        {
            if (machineInformation == null)
                return;


           // Records = TestData.GetRecords();
           Records = new ObservableCollection<Record>(MySQLDataAccess.GetRecords(machineInformation.Id));
        }
    }
}
