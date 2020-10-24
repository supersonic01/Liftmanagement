using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class MaintenanceAgreement : GoogleDrive ,IDatabaseObject
    {
        public long LocationId { get; set; }
        public long CustomerId { get; set; }
        public long MachineInformationId { get; set; }



        private DateTime duration = DateTime.Now;
        /// <summary>
        /// Laufzeit Vertrag bis 31.12.2019
        /// </summary>
        [DisplayName("Laufzeit Vertrag")]
        public DateTime Duration
        {
            get { return duration; }
            set
            {
                if (value.Date < Helper.Helper.DefaultDate.Date)
                {
                    value = DateTime.Now;
                }
                duration = value;
            }
        }

        /// <summary>
        /// jährlich Kündbar
        /// </summary>
        [DisplayName("Kündbar")]
        public string CanBeCancelled  { get; set; }

        /// <summary>
        /// durch Kunde
        /// </summary>
        [DisplayName("Vertrag gekündigt durch")]
        public string AgreementCancelledBy { get; set; }

        /// <summary>
        /// Frist 3 Monate vorher
        /// </summary>
        [DisplayName("Kündigungsfrist")]
        public int NoticeOfPeriod { get; set; } = 3;

        private DateTime agreementDate = DateTime.Now;
        /// <summary>
        /// abgeschlossen: 01/01/2011
        /// </summary>
        [DisplayName("Vertragsdatum")]
        public DateTime AgreementDate
        {
            get { return agreementDate; }
            set
            {
                if (value.Date < Helper.Helper.DefaultDate.Date)
                {
                    value = DateTime.Now;
                }
                agreementDate = value;
            }
        }

        /// <summary>
        /// Vollwartung, Systemwartung
        /// </summary>
        [DisplayName("Wartungsart")]
        public string MaintenanceType { get; set; }

        [DisplayName("Zusätzliche Informationen"), DatabaseAttribute(Length = "200")]
        public string AdditionalInfo { get; set; }
        //notice  agreement date

        /// <summary>
        /// 10, 14 Tage, Wochen
        /// </summary>
        [DisplayName("Benachrichtigung")]
        public int NotificationTime { get; set; }

        /// <summary>
        /// Tage, Wochen, Monte
        /// </summary>
        [DisplayName("Benachrichtigung")]
        public Helper.Helper.NotificationUnitType NotificationUnit { get; set; }

        public static string GetIndexFields()
        {
            return nameof(AgreementDate) + "," + nameof(NoticeOfPeriod) + "," + nameof(CanBeCancelled );
        }

        public override string GetFullName()
        {
            return string.Format("{0}, {1}, {2}", AgreementDate, NoticeOfPeriod, CanBeCancelled);
        }

        public override string ToString()
        {
            return GetFullName();
        }

    }
}
