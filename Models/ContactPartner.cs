﻿using Liftmanagement.Helper;
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
        public int ForeignKey { get; set; }
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

        public static string GetIndexFields()
        {
            return nameof(Name);
        }

    }
}
