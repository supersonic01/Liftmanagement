using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Helper
{
    //[AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class DatabaseAttribute : Attribute
    {
        public string Length { get; set; }
        public string Attribute { get; set; }
        public bool Updateable { get; set; } = true;

        public const string AUTO_INCREMENT = "NOT NULL AUTO_INCREMENT";
        public const string NOT_NULL = "NOT NULL";
        public const string UPDATE_TIMESTAMP = "DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP";
        public const string DEFAULT_TIMESTAMP = "DEFAULT CURRENT_TIMESTAMP";
        public const string UPDATE_DATETIME = "DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP";
        public const string DEFAULT_DATETIME = "DEFAULT CURRENT_TIMESTAMP";

        public DatabaseAttribute(string attribute)
        {
            this.Attribute = attribute;
        }
        public DatabaseAttribute()
        {

        }

       
    }
}
