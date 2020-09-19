using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class Location : Person, IDatabaseObject
    {             
        public long CustomerId { get; set; }      

        [DisplayName("Beim Störungsfall kontaktieren")]
        public bool ContactByDefect { get; set; }

        protected override string GetFullName()
        {
            return string.Format("{0}, {1} {2}", Address, Postcode, City);
        }

        public static string GetIndexFields()
        {
            return nameof(Address) + "," + nameof(Postcode) + "," + nameof(City);
        }
               
    }
}
