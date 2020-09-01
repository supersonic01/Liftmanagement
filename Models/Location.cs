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

        [DisplayName("Zusätzliche Informationen")]
        public string AdditionalInfo { get; set; }
    }
}
