using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class MaintenanceAgreement : BaseDatabaseField,IDatabaseObject
    {
        public long LocationId { get; set; }
        public long CustomerId { get; set; }
        public long MachineInformationId { get; set; }

        /// <summary>
        /// Laufzeit Vertrag bis 31.12.2019
        /// </summary>
        [DisplayName("Laufzeit Vertrag")]
        public DateTime Duration { get; set; }

        /// <summary>
        /// jährlich Kündbar
        /// </summary>
        [DisplayName("Kündbar")]
        public string CanBeCancelled  { get; set; }

        /// <summary>
        /// durch Kunde
        /// </summary>
        [DisplayName("Vertrag gekündigt durch")]
        public string ArreementCancelledBy { get; set; }

        /// <summary>
        /// Frist 3 Monate vorher
        /// </summary>
        [DisplayName("Kündigungsfrist")]
        public int NoticeOfPeriod { get; set; }

        /// <summary>
        /// abgeschlossen: 01/01/2011
        /// </summary>
        [DisplayName("Vertragsdatum")]
        public DateTime AgreementDate { get; set; }

        /// <summary>
        /// Vollwartung, Systemwartung
        /// </summary>
        [DisplayName("Wartungsart")]
        public string MaintenanceType { get; set; }

        [DisplayName("Zusätzliche Informationen"), DatabaseAttribute(Length = "200")]
        public string AdditionalInfo { get; set; }
        //notice  agreement date

        public static string GetIndexFields()
        {
            return nameof(AgreementDate) + "," + nameof(NoticeOfPeriod) + "," + nameof(CanBeCancelled );
        }
    }
}

/*
 
            lblLocationAdditionalInfo.Content = location.GetDisplayName<Location>(nameof(location.AdditionalInfo)) + ":";
            lblLocationContactPerson.Content = location.GetDisplayName<Location>(nameof(location.ContactPerson)) + ":";
            lblLocationAddress.Content = location.GetDisplayName<Location>(nameof(location.Address)) + ":";
            lblLocationPostcode.Content = location.GetDisplayName<Location>(nameof(location.Postcode)) + ":";
            lblLocationCity.Content = location.GetDisplayName<Location>(nameof(location.City)) + ":";
            lblLocationPhoneWork.Content = location.GetDisplayName<Location>(nameof(location.PhoneWork)) + ":";
            lblLocationMobile.Content = location.GetDisplayName<Location>(nameof(location.Mobile)) + ":";

            txtLocationAdditionalInfo.Text = location.AdditionalInfo;
            txtLocationContactPerson.Text = location.ContactPerson;
            txtLocationAddress.Text = location.Address;
            txtLocationPostcode.Text = location.Postcode;
            txtLocationCity.Text = location.City;
            txtLocationPhoneWork.Text = location.PhoneWork;
            txtLocationMobile.Text = location.Mobile;
 */