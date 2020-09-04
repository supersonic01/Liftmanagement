using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.ViewModels
{
  public  class GoogleDriveTreeNodeViewModel : ViewModel
    {

        public string Id { get; set; }
        public string _Name;
        public bool _Expand;
        public string Name
        {
            get { return _Name; }
            set { SetField(ref _Name, value); }
        }


        public bool Expand
        {
            get { return _Expand; }
            set { SetField(ref _Expand, value); }
        }

        private string webLink;

        public string WebLink
        {
            get { return webLink; }
            set { webLink = value; }
        }

        private int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }



        public ObservableCollection<GoogleDriveTreeNodeViewModel> Children { get; set; }
    }
}
