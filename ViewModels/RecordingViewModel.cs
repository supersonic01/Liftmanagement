using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
   public class RecordingViewModel:ViewModel
    {
    private Recording record = new Recording();

        public Recording Record
        {
            get { return record; }
            set { SetField(ref record, value); }
        }
    }
}
