using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
   public class OtherInformation: BaseDatabaseField 
    {       
        public long CustomerId { get; set; }
        public long MachineInformationId { get; set; }
        public string Text { get; set; }

        public OtherInformation(string text)
        {
            this.Text = text;
        }
    }
}
