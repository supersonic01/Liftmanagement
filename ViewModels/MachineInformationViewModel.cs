using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Data;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
public  class MachineInformationViewModel : ViewModel
    {

        //private Customer customerSelected = new Customer();

        //public Customer CustomerSelected
        //{
        //    get { return customerSelected; }
        //    set
        //    {
        //        SetField(ref customerSelected, value);
        //    }
        //}

        //private Location locationSelected = new Location();

        //public Location LocationSelected
        //{
        //    get { return locationSelected; }
        //    set { SetField(ref locationSelected, value); }
        //}

        private MachineInformation machineInformationSelected = new MachineInformation();

        public MachineInformation MachineInformationSelected
        {
            get { return machineInformationSelected; }
            set
            {
                SetField(ref machineInformationSelected, value);
            }
        }

        public MachineInformation MachineInformationSelectedLast { get; set; }

    
        public SQLQueryResult<MachineInformation> Add(Location location)
        {
            machineInformationSelected.ReadOnly = false;
            if (MachineInformationSelected.Id > 0)
            {
                return MySQLDataAccess.UpdateMachineInformation(MachineInformationSelected);
            }
            else
            {
                MachineInformationSelected.CustomerId = location.CustomerId;
                MachineInformationSelected.LocationId = location.Id;
                return MySQLDataAccess.AddMachineInformation(MachineInformationSelected);
            }

        }

        public SQLQueryResult<MachineInformation> EditMachineInformation()
        {
            var result = MySQLDataAccess.GetMachineInformationForEdit(MachineInformationSelected.Id);
            if (!result.IsReadOnly)
            {
                MachineInformationSelected = result.DBRecords.FirstOrDefault() as MachineInformation;
            }

            return result;
        }


        public SQLQueryResult<MachineInformation> ForceEditing()
        {
            return MySQLDataAccess.ForceToEditMachineInformation(MachineInformationSelected.Id);
        }


        public void ReleaseEditing()
        {
            Task.Factory.StartNew(() =>
            {
                MySQLDataAccess.ReleaseEditingMachineInformation(MachineInformationSelected.Id);
            });
        }

        public SQLQueryResult<MachineInformation> MarkForDeleteMachineInformation()
        {
            return MySQLDataAccess.MarkForDeleteMachineInformation(MachineInformationSelected);
        }
    }
}
