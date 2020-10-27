using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Liftmanagement.Data;
using Liftmanagement.Helper;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
    public partial class ManagementOverviewFilterViewModel : ViewModel
    {
        private ObservableCollection<ManagementOverviewFilter> managementOverviews;

        public ObservableCollection<ManagementOverviewFilter> ManagementOverviews
        {
            get { return managementOverviews; }
            set { managementOverviews = value; }
        }

        private ICollectionView managementOverviewView = null;

        public ICollectionView ManagementOverviewView
        {
            get
            {
                if (managementOverviewView == null)
                {
                    var _itemSourceList = new CollectionViewSource()
                        { Source = ManagementOverviews };
                    managementOverviewView = _itemSourceList.View;
                }

                return managementOverviewView;
            }
            set { SetField(ref managementOverviewView, value); }
        }

        public ManagementOverviewFilterViewModel()
        {
            managementOverviews = MySQLDataAccess.GetManagementOverviewFilter();
        }

    }
}
