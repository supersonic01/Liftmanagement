using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Helper
{
  public static  class Helper
    {
        public enum TTypeMangement
        {
            Customer = 0,
            Location = 1,
            MaintenanceAgreement = 2,
            EmergencyAgreement = 3,
            MachineInformation = 5,
            Task = 7,
            Profil = 9,
            Update = 11,
            Managment = 13,
        }

        public enum VarietyType
        {
            MACancellaration =0, //jährich monatlich
            MACancelledBy = 1, //kunde
            MAType=2 //Vollwartung, Systemwartung

        }

        private static Dictionary<Type, int> classTypeForeignKeyTypeMapper;

        public static Dictionary<Type, int> ClassTypeForeignKeyTypeMapper
        {
            get {
                if (classTypeForeignKeyTypeMapper == null)
                {
                    classTypeForeignKeyTypeMapper = GetClassTypeForeignKeyTypeMapper();
                }
                return classTypeForeignKeyTypeMapper; }
           
        }

        private static Dictionary<Type, string> dataTypRederStringMapper;

        public static Dictionary<Type, string> DataTypRederStringMapper
        {
            get
            {
                if (dataTypRederStringMapper == null)
                {
                    dataTypRederStringMapper = GetDataTypRederString();
                }
                return dataTypRederStringMapper;
            }

        }


        public static void GetTabelValueHeaders(int amount)
        {
            string header="";
            for (int i = 0; i < amount; i++)
            {
                header = header + "[{"+i+"}],";
            }
            Console.WriteLine(header);
        }

       public static void GenerateInsert(Type databaseObject)
        {

            StringBuilder insertScript = new StringBuilder();
            StringBuilder insertScriptValues = new StringBuilder();

            Type classType = databaseObject;
            string className = classType.Name.ToLower();
            string tableName = className.ToUpper();

            insertScript.Append("INSERT INTO " + tableName);
            insertScript.Append("(");

            insertScriptValues.Append("VALUE(");

            foreach (PropertyInfo p in classType.GetProperties())
            {
                bool updateableColumne = true;

                var attribute = p.GetCustomAttributes(typeof(DatabaseAttribute), true)
                    .Cast<DatabaseAttribute>().FirstOrDefault();

                if (attribute != null && attribute.Updateable == false)
                {
                    updateableColumne = false;
                }

                if (!p.DeclaringType.IsAbstract && !p.DeclaringType.IsInterface && updateableColumne)
                {
                    insertScript.Append(p.Name);
                    insertScript.Append(",");

                    if (p.PropertyType == typeof(string) || p.PropertyType == typeof(DateTime))
                    {
                        insertScriptValues.Append(string.Format("'\"+{0}+\"',", className + "." + p.Name));
                    }
                    else
                    {
                        insertScriptValues.Append(string.Format("\"+{0}+\",", className + "." + p.Name));
                    }
                }
            }

            insertScript.Append(")");
            insertScriptValues.Append(")");


            Console.WriteLine(RemoveLastComma(insertScript.ToString()));
            Console.WriteLine(RemoveLastComma(insertScriptValues.ToString()));
        }

        public static void GenerateSelect(Type databaseObject)
        {          
            StringBuilder insertScriptValues = new StringBuilder();

            Type classType = databaseObject;
            string className = classType.Name.ToLower();
            string tableName = className.ToUpper();


            insertScriptValues.AppendLine(string.Format("List<{0}> {1}s = new List<{0}>();", databaseObject.Name, className));
            insertScriptValues.AppendLine(string.Format("{0} {1} = new {0}();", databaseObject.Name, className));                   

            foreach (PropertyInfo p in classType.GetProperties())
            {
                bool updateableColumne = true;

                var attribute = p.GetCustomAttributes(typeof(DatabaseAttribute), true)
                    .Cast<DatabaseAttribute>().FirstOrDefault();

                if (attribute != null && attribute.Updateable == false)
                {
                    updateableColumne = false;
                }

                if (!p.DeclaringType.IsAbstract && !p.DeclaringType.IsInterface)
                {

                    if (p.PropertyType != typeof(Timestamp) && p.PropertyType != typeof(ContactPartner) && p.PropertyType != typeof(AdministratorCompany))
                    {
                        insertScriptValues.AppendLine(string.Format("{0}.{1} = reader.{2}(\"{3}\");", className, p.Name, GetReaderString(p.PropertyType), p.Name));
                    }
                }              
            }

            insertScriptValues.AppendLine(string.Format("{0}s.Add({0});", className));

            Console.WriteLine(RemoveLastComma(insertScriptValues.ToString()));
        }

        private static string GetReaderString(Type propertyType)
        {
            string readerString ="";
            if (DataTypRederStringMapper.ContainsKey(propertyType))
            {
                readerString = DataTypRederStringMapper[propertyType];
            }
            return readerString;
        }

        private static string RemoveLastComma(string value)
        {
            int index = value.IndexOf(",", value.Length - 3);
            string cleanString = (index < 0)
                ? value
                : value.Remove(index, ",".Length);

            return cleanString;
        }

        public static int GetForeignKeyType(BaseDatabaseField baseField)
        {
            Type classType = baseField.GetType();
            int foreignKeyType=-1;
            if (ClassTypeForeignKeyTypeMapper.ContainsKey(classType)){
               foreignKeyType= ClassTypeForeignKeyTypeMapper[classType];
            }
            else
            {
                throw new Exception("ForeignKeyType not defined");
            }
            return foreignKeyType;
        }

        private static Dictionary<Type, int> GetClassTypeForeignKeyTypeMapper()
        {
            Dictionary<Type, int> typeMapper = new Dictionary<Type, int>();
            typeMapper.Add(typeof(Customer), 0);
            typeMapper.Add(typeof(AdministratorCompany), 1);
            typeMapper.Add(typeof(Location), 2);
            typeMapper.Add(typeof(MachineInformation), 3);
            return typeMapper;
        }

        private static Dictionary<Type, string> GetDataTypRederString()
        {
            Dictionary<Type, string> typeMapper = new Dictionary<Type, string>();
            typeMapper.Add(typeof(string), "GetString");
            typeMapper.Add(typeof(long), "GetInt64");
            typeMapper.Add(typeof(int), "GetInt32");
            typeMapper.Add(typeof(bool), "GetBoolean");
            typeMapper.Add(typeof(DateTime), "GetDateTime");
            typeMapper.Add(typeof(decimal), "GetDecimal");
            return typeMapper;
        }

    }


}
