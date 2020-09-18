using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Data
{
  public class SQLQueryResult
    {
        public struct SQLSubQueryResult
        {
            int Records { get; }
            long Id { get; }
            Type ClaseType { get; set; }
        }

        public List<SQLSubQueryResult> SQLSubQueryResults { get; set; } = new List<SQLSubQueryResult>();

        public int Records { get; }
        public long Id { get; }

       

        public SQLQueryResult(int records, long id)
        {
            Records = records;
            Id = id;
        }

       
    }

    
}
