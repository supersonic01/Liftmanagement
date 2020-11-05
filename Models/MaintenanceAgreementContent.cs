using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Helper;

namespace Liftmanagement.Models
{
   public class MaintenanceAgreementContent: BaseDatabaseField,IDatabaseObject
    {
        public long MaintenanceAgreementId { get; set; }
        public long CustomerId { get; set; }
        public long MachineInformationId { get; set; }

        public int Sequence { get; set; }

        private string content ;

        [DisplayName("Inhalt"), DatabaseAttribute(Length = "200")]
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private string provide = "Ja";
        [DisplayName(""), Database(Length = "50")]
        public string Provide
        {
            get { return provide; }
            set { provide = value; }
        }

        private bool reuseContent;
        [DisplayName("Wiederverwenden")]
        public bool ReuseContent
        {
            get { return reuseContent; }
            set { reuseContent = value; }
        }

        public static string GetIndexFields()
        {
            return nameof(Content);
        }
    }
}
