using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Liftmanagement.Models
{

    public class ContactPartner : BaseDatabaseField, IDatabaseObject
    {
        public long CustomerId { get; set; } = -1;
        public long ForeignKey { get; set; }
        public int ForeignKeyType { get; set; }
     
        [DatabaseAttribute(DatabaseAttribute.NOT_NULL)]
        [DisplayName("Ansprechpartner"), Display(DataGridColumnWidth = DataGridLengthUnitType.Star)]
        public string Name { get; set; }
               
        [DisplayName("Tel. geschäftli."), Display(DataGridColumnWidth = DataGridLengthUnitType.Star)]
        public string PhoneWork { get; set; }

        [DisplayName("Mobil"), Display(DataGridColumnWidth = DataGridLengthUnitType.Star)]
        public string Mobile { get; set; }

        [DisplayName("E-Mail"), Display(DataGridColumnWidth = DataGridLengthUnitType.Star)]
        public string EMail { get; set; }

        [DisplayName("Beim Störungsfall kontaktieren"), Display(Order = 9,DataGridColumnWidth = DataGridLengthUnitType.Star,LengthValue = 1.2)]
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
