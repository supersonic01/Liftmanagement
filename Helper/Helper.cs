using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Helper
{
  public static  class Helper
    {
        public enum TTypeMangement
        {
            Customer = 0,
            Location = 1,
            MaintenanceAgreement = 2,
            EmergencyAgreement = 3,
            MachineInformation = 5,
            Task = 7,
            Profil = 9,
            Update = 11,
            Managment = 13,
        }

        public enum TTypeTermination
        {
            Jährlich =0,
            Monatlich =1
        }
    }
}
