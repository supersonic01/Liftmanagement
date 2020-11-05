using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Models;
using Liftmanagement.Reports;
using SAPBusinessObjects.WPF.Viewer;

namespace Liftmanagement.Reports
{
    public class CostSavingReport
    {
        private ReportViewer reportViewerWindow = null;

        public CostSavingReport(List<Record> records, Customer customer,Location location, MachineInformation machineInformation)
        {
           var cp = new CostSaving();
            // var cp = new CrystalReport1();

            List<RptRecord> rptRecords = new List<RptRecord>();

            foreach (var record in records)
            {
                rptRecords.Add(new RptRecord { BillingAmountCorrect = record.BillingAmountCorrect, CostType = record.CostType, InvoiceNumber = record.InvoiceNumber, OfferPrice = record.OfferPrice, Process = record.Process });
            }

            cp.SetDataSource(rptRecords);
            cp.SetParameterValue("pCompanyName", customer.CompanyName);
            cp.SetParameterValue("pAddress", customer.Address);
            cp.SetParameterValue("pPostcode", customer.Postcode);
            cp.SetParameterValue("pCity", customer.City);

            cp.SetParameterValue("pName", machineInformation.Name);
            cp.SetParameterValue("pSerialNumber", machineInformation.SerialNumber);
            cp.SetParameterValue("pYearOfConstruction", machineInformation.YearOfConstruction);
            cp.SetParameterValue("pHoldingPositions", machineInformation.HoldingPositions);
            cp.SetParameterValue("pEntrances", machineInformation.Entrances);
            cp.SetParameterValue("pPayload", machineInformation.Payload);
            
            cp.SetParameterValue("pLAddress", location.Address);
            cp.SetParameterValue("pLPostcode", location.Postcode);
            cp.SetParameterValue("pLCity", location.City);



            if (reportViewerWindow == null)
            {
                reportViewerWindow = new ReportViewer(cp);
            }
            
             reportViewerWindow.Closing += Dd_Closing;
             reportViewerWindow.ShowDialog();

        }

        private void Dd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
          reportViewerWindow.Hide();
        }
    }
}
