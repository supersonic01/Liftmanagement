using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Data;

namespace Liftmanagement.ViewModels
{
   public class MachineInformationsViewModel : ViewModel
    {

        private List<MachineInformation> machineInformations = new List<MachineInformation>();

        public List<MachineInformation> MachineInformations
        {
            get { return machineInformations; }
            set { SetField(ref machineInformations, value); }
        }


        public MachineInformationsViewModel()
        {
           
        }

        public void RefreshByLocation(long id)
        {
            MachineInformations = MySQLDataAccess.GetMachineInformationsByLocation(id);
        }
    }
}
