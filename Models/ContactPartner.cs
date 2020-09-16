using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{

    public class ContactPartner : DisplayNameRetriever
    {
        [DisplayName("Ansprechpartner")]
        public string Name { get; set; }

        [DisplayName("Tel. geschäftli.")]
        public string PhoneWork { get; set; }

        [DisplayName("Mobil")]
        public string Mobile { get; set; }

        [DisplayName("E-Mail")]
        public string EMail { get; set; }

    }
}
