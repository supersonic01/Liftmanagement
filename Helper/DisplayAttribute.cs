using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Liftmanagement.Helper
{
   public class DisplayAttribute : Attribute
   {
       public int Order { get; set; } = -1;
       public DataGridLengthUnitType DataGridColumnWidth { get; set; } 
   }
}
