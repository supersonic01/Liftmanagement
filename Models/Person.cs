using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class Person
    {
        [DisplayName("Ansprechpartner")]
        public string ContactPerson { get; set; }
        [DisplayName("Adresse")]
        public string Address { get; set; }
        [DisplayName("PLZ")]
        public string Postcode { get; set; }
        [DisplayName("Stadt")]
        public string City { get; set; }
        [DisplayName("Tel. geschäftli.")]
        public string PhoneWork { get; set; }
        [DisplayName("Mobil")]
        public string Mobile { get; set; }
        [DisplayName("Merken")]
        public bool Selected { get; set; }


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
    }

}
