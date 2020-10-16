using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Helper;

namespace Liftmanagement.Models
{
    public class Location : Person, IDatabaseObject
    {             
        public long CustomerId { get; set; }      

        //[DisplayName("Beim Störungsfall kontaktieren"),Display(Order = 9)]
        //public bool ContactByDefect { get; set; }

        public override string GetFullName()
        {
            return string.Format("{0}, {1} {2}", Address, Postcode, City);
        }

        public static string GetIndexFields()
        {
            return nameof(Address) + "," + nameof(Postcode) + "," + nameof(City);
        }

        public override string ToString()
        {
            return GetFullName();
        }
    }
}
