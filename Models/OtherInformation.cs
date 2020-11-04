using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Helper;

namespace Liftmanagement.Models
{
    public class OtherInformation : BaseDatabaseField,IDatabaseObject
    {
        public long CustomerId { get; set; }
        public long MachineInformationId { get; set; }

        public int Sequence { get; set; }

        [DatabaseAttribute(IsDbColumn = false, Updateable = false)]
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

        public static string GetIndexFields()
        {
            return nameof(Text) ;
        }
    }
}