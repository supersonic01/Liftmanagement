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
             internal int Records { get; set; }
            internal long Id { get; set; }
            internal Type PrimaryClassType { get; set; }
            internal Type ForeignClassType { get; set; }
        }

        public List<SQLSubQueryResult> SQLSubQueryResults { get; set; } = new List<SQLSubQueryResult>();

        public int Records { get; }
        public long Id { get; }
        Type ClassType { get; }



        public SQLQueryResult(int records, long id, Type classType)
        {
            Records = records;
            Id = id;
            this.ClassType = classType;
        }

        internal void AddSQLSubQueryResult(SQLQueryResult result, SQLQueryResult primaryResult)
        {
            SQLSubQueryResults.Add(
                new SQLSubQueryResult
                {
                    Records = result.Records, Id = result.Id, PrimaryClassType = primaryResult.ClassType,
                    ForeignClassType = result.ClassType
                });

        }
    }

    
}
