using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Models;

namespace Liftmanagement.Data
{
  public class SQLQueryResult<T> where T :class
    {
        public struct SQLSubQueryResult
        {
             internal int Records { get; set; }
            internal long Id { get; set; }
            internal Type PrimaryClassType { get; set; }
            internal Type ForeignClassType { get; set; }
        }

       // public List<SQLSubQueryResult> SQLSubQueryResults { get; set; } = new List<SQLSubQueryResult>();

        public int Records { get; }
        public long Id { get; }
        internal bool IsReadOnly { get; set; }
        internal string CurrentlyUsedBy { get; set; }
        internal List<T> DBRecords { get; set; }


        public SQLQueryResult(int records, long id)
        {
            Records = records;
            Id = id;
        }

        //internal void AddSQLSubQueryResult(SQLQueryResult<T> result, SQLQueryResult<T> primaryResult)
        //{
        //    SQLSubQueryResults.Add(
        //        new SQLSubQueryResult
        //        {
        //            Records = result.Records, Id = result.Id,
        //        });

      //  }

       }

    
}
