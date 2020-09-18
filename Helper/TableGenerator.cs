using Liftmanagement.Models;
using MySqlX.XDevAPI.CRUD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Liftmanagement.Helper
{


    public class TableGenerator
    {
        //https://stackoverflow.com/questions/47239/how-can-i-generate-database-tables-from-c-sharp-classes

        private StringBuilder creatScript;

        public StringBuilder CreatScript
        {
            get { return creatScript; }
            set { creatScript = value; }
        }

       // public List<StringBuilder> CreatScripts { get; set; } = new List<StringBuilder>();
        

        public TableGenerator()
        {
            List<TableClass> tables = new List<TableClass>();

             creatScript = new StringBuilder();

            Assembly a = Assembly.GetExecutingAssembly();
            string nameSpace = "Liftmanagement.Models";

            List<Type> classTyppes = new List<Type>();

            var types = a.GetTypes().Where(c => c.Namespace == nameSpace && typeof(IDatabaseObject).IsAssignableFrom(c));

           
            foreach (Type type in types)
            {
                if (!type.IsInterface)
                {
                    //if(scriptCount > 3)
                    //{
                    //    CreatScripts.Add(creatScript);
                    //    creatScript = new StringBuilder();
                    //    scriptCount = 0;

                    //}
                    //CreatScripts.Add(new StringBuilder().Append(new TableClass(type).CreateTableScript()));
                    creatScript.Append(new TableClass(type).CreateTableScript());
                    // scriptCount++;
                }
            }

            //foreach (var script in CreatScripts)
            //{
            //    Console.WriteLine(script.ToString());
            //}

            Console.WriteLine(creatScript.ToString());

        }

    }

    public class TableClass
    {
        private List<KeyValuePair<String, Type>> _fieldInfo = new List<KeyValuePair<String, Type>>();
        private string _className = String.Empty;

        private Dictionary<Type, String> dataMapper
        {
            get
            {
                // Add the rest of your CLR Types to SQL Types mapping here
                Dictionary<Type, String> dataMapper = new Dictionary<Type, string>();
                dataMapper.Add(typeof(long), "BIGINT");
                dataMapper.Add(typeof(int), "INT");
                dataMapper.Add(typeof(string), "VARCHAR(50)");
                dataMapper.Add(typeof(bool), "BOOLEAN");
                dataMapper.Add(typeof(DateTime), "DATETIME");
                dataMapper.Add(typeof(float), "FLOAT");
                dataMapper.Add(typeof(decimal), "DECIMAL(18,0)");
                dataMapper.Add(typeof(Guid), "UNIQUEIDENTIFIER");
                dataMapper.Add(typeof(Timestamp), "TIMESTAMP");

                return dataMapper;
            }
        }

        public List<KeyValuePair<String, Type>> Fields
        {
            get { return this._fieldInfo; }
            set { this._fieldInfo = value; }
        }

        public string ClassName
        {
            get { return this._className; }
            set { this._className = value; }
        }

        public Type ClassType { get; set; }

        public TableClass(Type t)
        {
            this._className = t.Name;
            ClassType = t;

            foreach (PropertyInfo p in t.GetProperties())
            {
                if (!p.DeclaringType.IsAbstract && !p.DeclaringType.IsInterface)
                {
                    KeyValuePair<String, Type> field = new KeyValuePair<String, Type>(p.Name, p.PropertyType);

                    this.Fields.Add(field);
                }
            }
        }

        private string getCustomizedDatatypeLength(string value, DatabaseAttribute attribute)
        {

            if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Length))
            {
                value = value.Replace("50", attribute.Length);
            }

            return value;
        }


        public string CreateTableScript()
        {
            System.Text.StringBuilder script = new StringBuilder();

            script.AppendLine("CREATE TABLE " + this.ClassName);
            script.AppendLine("(");
            // script.AppendLine("\t ID BIGINT,");
            for (int i = 0; i < this.Fields.Count; i++)
            {
                KeyValuePair<String, Type> field = this.Fields[i];

                if (dataMapper.ContainsKey(field.Value))
                {
                    MemberInfo property = ClassType.GetProperty(field.Key);
                    var attribute = property.GetCustomAttributes(typeof(DatabaseAttribute), true)
                        .Cast<DatabaseAttribute>().FirstOrDefault();

                    string value = getCustomizedDatatypeLength(dataMapper[field.Value], attribute);
                    script.Append("\t " + field.Key + " " + value);


                    if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Attribute))
                    {
                        script.Append(" " + attribute.Attribute);
                    }

                    script.Append(",");

                    script.Append(Environment.NewLine);

                }
            }

            script.AppendLine("CONSTRAINT " + this.ClassName + "_pk PRIMARY KEY (ID)");

            string index = (string)ClassType.GetMethod("GetIndexFields").Invoke(null, null);

            if (!string.IsNullOrWhiteSpace(index))
            {
                script.Append(",");
                script.AppendLine("INDEX " + this.ClassName + "_idx (" + index + ")");
            }


            script.AppendLine(");");

            return script.ToString().ToUpper();
        }
    }



}
