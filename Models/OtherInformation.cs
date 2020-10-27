using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class OtherInformation : BaseDatabaseField
    {
        public long CustomerId { get; set; }
        public long MachineInformationId { get; set; }
        public bool TextChanged { get; set; }
   
        private string text;

        public string Text
        {
            get { return text; }
            set
            {
                SetField(ref text, value);

            }
        }


        public OtherInformation(string text)
        {
            this.text = text;
        }

        public OtherInformation()
        {

        }
    }
}