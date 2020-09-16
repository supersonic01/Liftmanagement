using Liftmanagement.Helper;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.ViewModels
{
   public class MachineInformationsViewModel : ViewModel
    {

        private List<MachineInformation> machineInformations = new List<MachineInformation>();

        public List<MachineInformation> MachineInformations
        {
            get { return machineInformations; }
            set { machineInformations = value; }
        }


        public MachineInformationsViewModel()
        {
            machineInformations = TestData.GetMachineInformations();
        }   
    }
}
