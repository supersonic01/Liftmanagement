using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Liftmanagement.Models
{
  public class BaseDatabaseField: DisplayNameRetriever
    {
        [DatabaseAttribute(DatabaseAttribute.AUTO_INCREMENT, Updateable =false)]
        public long Id { get; set; }

        [DatabaseAttribute(DatabaseAttribute.DEFAULT_DATETIME, Updateable = false)]
        public DateTime CreatedDate { get; set; }
        [DatabaseAttribute(DatabaseAttribute.UPDATE_DATETIME, Updateable = false)]
        public DateTime ModifiedDate { get; set; }
        [DatabaseAttribute(DatabaseAttribute.UPDATE_TIMESTAMP, Updateable = false)]
        public Timestamp Timestamp { get; set; }
        [DatabaseAttribute(DatabaseAttribute.NOT_NULL)]
        public string CreatedPersonName { get; set; }
        [DatabaseAttribute(DatabaseAttribute.NOT_NULL)]
        public string ModifiedPersonName { get; set; }
       
        public bool ReadOnly { get; set; }       
        public string UsedBy { get; set; }
    }
}
