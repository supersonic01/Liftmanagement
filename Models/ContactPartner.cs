using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{

    public class ContactPartner : BaseDatabaseField, IDatabaseObject
    {
        public long CustomerId { get; set; } = -1;
        public long ForeignKey { get; set; }
        public int ForeignKeyType { get; set; }
     
        [DatabaseAttribute(DatabaseAttribute.NOT_NULL)]
        [DisplayName("Ansprechpartner")]
        public string Name { get; set; }
               
        [DisplayName("Tel. geschäftli.")]
        public string PhoneWork { get; set; }

        [DisplayName("Mobil")]
        public string Mobile { get; set; }

        [DisplayName("E-Mail")]
        public string EMail { get; set; }

        [DisplayName("Beim Störungsfall kontaktieren"), Display(Order = 9)]
        public bool ContactByDefect { get; set; }

        public static string GetIndexFields()
        {
            return nameof(Name);
        }

        public ContactPartner(BaseDatabaseField baseField)
        {
            CreatedPersonName = baseField.CreatedPersonName;
            ModifiedPersonName = baseField.ModifiedPersonName;
            ReadOnly = baseField.ReadOnly;
            UsedBy = baseField.UsedBy;

            ForeignKeyType = Helper.Helper.GetForeignKeyType(baseField);
        }

        public ContactPartner()
        {
        }
    }
}
