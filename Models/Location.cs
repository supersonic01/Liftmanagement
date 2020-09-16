using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class Location : Person
    {

        public int LocationId { get; set; }
        public int CustomerId { get; set; }
      

        [DisplayName("Beim Störungsfall kontaktieren")]
        public bool ContactByDefect { get; set; }

        protected override string GetFullName()
        {
            return string.Format("{0}, {1} {2}", Address, Postcode, City);
        }
    }
}
