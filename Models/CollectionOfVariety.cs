using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Liftmanagement.Models
{
   public class CollectionOfVariety :  IDatabaseObject
    {
        [DatabaseAttribute(DatabaseAttribute.AUTO_INCREMENT, Updateable = false)]
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public int VarietyType { get; set; }
        public int Sequence { get; set; }

        public static string GetIndexFields()
        {
            return null;
        }
    }



}
