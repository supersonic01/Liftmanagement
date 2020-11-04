using Liftmanagement.Data;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Liftmanagement.Helper.Helper;

namespace Liftmanagement.ViewModels
{
    public class CategoryViewModel : ViewModel
    {
        private ObservableCollection<Category> categories = new ObservableCollection<Category>();

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        public CategoryViewModel()
        {
            categories.Add(new Category("Verwaltung", @"\Resources\Images\Icons\Maintenance.ico",TTypeMangement.Managment));
            categories.Add(new Category("Kunden", @"\Resources\Images\Icons\Office-Customer-Male-Light.ico", TTypeMangement.Customer));
           // categories.Add(new Category("Standorte", @"\Resources\Images\Icons\Pixelkit-Flat-Jewels-Location.ico", TTypeMangement.Location));
            categories.Add(new Category("Anlagen", @"\Resources\Images\Icons\elevator-symbol-1444871.jpg", TTypeMangement.MachineInformation));
            categories.Add(new Category("Wartungsverträge", @"\Resources\Images\Icons\Aha-Soft-Software-Signature.ico", TTypeMangement.MaintenanceAgreement));
            categories.Add(new Category("Notrufverträg", @"\Resources\Images\Icons\Iconshock-Real-Vista-Medical-Emergency.ico", TTypeMangement.EmergencyAgreement));
            categories.Add(new Category("Report", @"\Resources\Images\Icons\Aha-Soft-Large-Seo-SEO.ico", TTypeMangement.Report));
        }
    }
}
