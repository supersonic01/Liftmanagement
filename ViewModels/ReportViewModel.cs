using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Liftmanagement.Data;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
    public class ReportViewModel : ViewModel
    {
        private List<Record> records = new List<Record>();

        public List<Record> Records
        {
            get { return records; }
            set { SetField(ref records, value); }
        }


        private double offerPriceSum;

        public double OfferPriceSum
        {
            get { return offerPriceSum; }
            set { SetField(ref offerPriceSum, value); }
        }

        private double billingAmountCorrecteSum;

        public double BillingAmountCorrecteSum
        {
            get { return billingAmountCorrecteSum; }
            set { SetField(ref billingAmountCorrecteSum, value); }
        }

        private double savingCostSum;

        public double SavingCostSum
        {
            get { return savingCostSum; }
            set { SetField(ref savingCostSum, value); }
        }

        private bool yearChecked= true;

        public bool YearChecked
        {
            get { return yearChecked; }
            set { SetField(ref yearChecked, value); }
        }

        private List<Record> RecordsMa { get; set; }

        private ObservableCollection<string> years = new ObservableCollection<string>();

        public ObservableCollection<string> Years
        {
            get { return years; }
            set
            {
                SetField(ref years, value);
            }
        }


        private DateTime reportStart = DateTime.Now;

        public DateTime ReportStart
        {
            get { return reportStart; }
            set { SetField(ref reportStart, value); }
        }

        private DateTime reportEnd = DateTime.Now;

        public DateTime ReportEnd
        {
            get { return reportEnd; }
            set { SetField(ref reportEnd, value); }
        }

        public string YearSelected { get; set; }

        public void RefreshMachine(MachineInformation machineInformation)
        {
            if (machineInformation == null)
                return;

            RecordsMa = MySQLDataAccess.GetRecords(machineInformation.Id);

            FillCombobox();
        }

        public void Refresh()
        {
            DateTime dateStart = reportStart;
            DateTime dateEnd = reportEnd.AddDays(1);
            if (yearChecked)
            {
                if(string.IsNullOrWhiteSpace(YearSelected))
                    return;
                //Take Year
                dateStart= new DateTime(int.Parse(YearSelected), 01,01);
                dateEnd = new DateTime(((int.Parse(YearSelected))+1), 01, 01);
            }
          

           Records = RecordsMa.Where(c=>c.Date >= dateStart && c.Date < dateEnd).ToList();

           BillingAmountCorrecteSum= records.Sum(c => c.BillingAmountCorrect);
           OfferPriceSum = records.Sum(c => c.OfferPrice);
           SavingCostSum = OfferPriceSum - BillingAmountCorrecteSum;
        }

        private void FillCombobox()
        {

            Task.Factory.StartNew(() =>
            {
              var dd=  RecordsMa.Select(c => c.Date.ToString("yyyy")).Distinct().ToList();

              Years = new ObservableCollection<string>(dd);

            });
        }
    }
}
