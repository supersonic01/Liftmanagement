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
        [DatabaseAttribute(DatabaseAttribute.AUTO_INCREMENT)]
        public Int64 Id { get; set; }

        [DatabaseAttribute(DatabaseAttribute.DEFAULT_DATETIME)]
        public DateTime CreatedDate { get; set; }
        [DatabaseAttribute(DatabaseAttribute.UPDATE_DATETIME)]
        public DateTime ModifiedDate { get; set; }
        [DatabaseAttribute(DatabaseAttribute.UPDATE_TIMESTAMP)]
        public Timestamp Timestamp { get; set; }
        [DatabaseAttribute(DatabaseAttribute.NOT_NULL)]
        public string CreatedPersonName { get; set; }
        [DatabaseAttribute(DatabaseAttribute.NOT_NULL)]
        public string ModifiedPersonName { get; set; }
       
        public bool ReadOnly { get; set; }       
        public string UsedBy { get; set; }
    }
}
