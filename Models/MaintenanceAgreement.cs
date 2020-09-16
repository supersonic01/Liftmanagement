using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
    public class MaintenanceAgreement : DisplayNameRetriever
    {
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public int MachineInformationId { get; set; }
        public int MaintenanceAgreementId { get; set; }

        [DisplayName("Laufzeit Vertrag")]
        public string Duration { get; set; }

        [DisplayName("Kündbar")]
        public string Terminated { get; set; }

        [DisplayName("Kündigungsfrist")]
        public string NoticeOfPeriod { get; set; }

        [DisplayName("Vertragsdatum")]
        public string AgreementDate { get; set; }

        [DisplayName("Zusätzliche Informationen")]
        public string AdditionalInfo { get; set; }
        //notice  agreement date


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