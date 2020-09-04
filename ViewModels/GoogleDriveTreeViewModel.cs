using Liftmanagement.BusinessLogic.GoogleManagement;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Liftmanagement.ViewModels
{
    public class GoogleDriveTreeViewModel : ViewModel
    {
        public ObservableCollection<GoogleDriveTreeNodeViewModel> Items { get; set; }

        List<GoogleDriveTreeNodeViewModel> nodesList = new List<GoogleDriveTreeNodeViewModel>();


        public GoogleDriveTreeViewModel()
        {
            List<GoogleDriveFolder> googleDriveFolders = new DriveLiftmanagement().GoogleDriveFolders;


            if (googleDriveFolders.Count > 0)
            {
                googleDriveFolders.Where(c => c.ParentId == null).ToList()
                     .ForEach(d => nodesList.Add(new GoogleDriveTreeNodeViewModel
                     {
                         Id = d.Id,
                         Name = d.Name,
                         Expand = true,
                         WebLink= d.WebLink,
                         Level = 0,
                         Children = new ObservableCollection<GoogleDriveTreeNodeViewModel>()
                     }));

                Items = new ObservableCollection<GoogleDriveTreeNodeViewModel>();

                foreach (var item in nodesList)
                {
                    Items.Add(item);
                }

                int level = 0;

                NodesFactory(googleDriveFolders, level);
            }
            else { MessageBox.Show("List of Direcitory is empty"); }
        }

        public void NodesFactory(List<GoogleDriveFolder> googleDriveFolders, int level)
        {

            level = level + 1;

            var querryLevel =
            from folderItems in googleDriveFolders
            join itemSub in nodesList on folderItems.ParentId equals itemSub.Id
            where itemSub.Level == level - 1
            select new { Id = folderItems.Id, Name = folderItems.Name, ParentId = folderItems.ParentId, Level = level , WebLink = folderItems.WebLink };

            foreach (var x in querryLevel)
            {

                nodesList.Add(new GoogleDriveTreeNodeViewModel { Id = x.Id, Name = x.Name, Level = x.Level, WebLink = x.WebLink, Expand = true, Children = new ObservableCollection<GoogleDriveTreeNodeViewModel>() });

                nodesList.Find(gNode => gNode.Id == x.ParentId).Children.Add(nodesList.Last());
            }

            if (querryLevel.Count() > 0)
            { NodesFactory(googleDriveFolders, level); }
        }
    }
}
