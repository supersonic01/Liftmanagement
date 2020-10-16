using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public abstract class DisplayNameRetriever
    {

        public string FullName
        {
            get { return GetFullName(); }

        }

        public virtual string GetFullName()
        {
            throw new NotImplementedException();
        }

        public string GetDisplayName<T>(string propertyName)
        {
            MemberInfo property = typeof(T).GetProperty(propertyName);
            var attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Cast<DisplayNameAttribute>().Single();

            if (attribute.DisplayName != null)
            {
                return attribute.DisplayName;
            }
            return string.Empty;
        }

        public static List<string> GetPropertiesStringNames<T>(List<string> propertiesToAvoid)
        {
            List<string> propertiesStringNames = new List<string>();

            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties();
            // sort properties by name
            Array.Sort(propertyInfos,
                    delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                    { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Console.WriteLine(propertyInfo.Name);

                if (!propertiesToAvoid.Contains(propertyInfo.Name))
                {
                    propertiesStringNames.Add(propertyInfo.Name);
                }
            }

            return propertiesStringNames;
        }


    }
}
