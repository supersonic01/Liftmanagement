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
            List<GoogleDriveFolder> GoogleDriveFolders = new DriveLiftmanagement().GoogleDriveFolders;
           

            if (GoogleDriveFolders.Count > 0)
            {

                //datatable to load data from GoogleDriveFolder_List
                DataTable InputDT = new DataTable();
                InputDT.Columns.Add("Gid", typeof(string));
                InputDT.Columns.Add("Gname", typeof(string));
                InputDT.Columns.Add("GparentId", typeof(string));

                //loading the data from  to datable
                foreach (var x in GoogleDriveFolders)
                {
                    DataRow rowInput = InputDT.NewRow();
                    rowInput["Gid"] = x.Id;
                    rowInput["Gname"] = x.Name;
                    rowInput["GparentId"] = x.ParentId;
                    InputDT.Rows.Add(rowInput);
                }

                //querring the datable to find the row with root as first level of hierachy
                var queryRoot =
                      from itemsInputDT in InputDT.AsEnumerable()
                      where itemsInputDT.Field<string>("GparentId") == null
                      select new { Gid = itemsInputDT.Field<string>("Gid"), Gname = itemsInputDT.Field<string>("Gname"), GparentId = itemsInputDT.Field<string>("GparentId"), level = 1 };



                //creating auxiliary datatable for iterationaly querying in the subsequent levels of hierarchy
                DataTable sub = new DataTable();
                sub.Columns.Add("Gid", typeof(string));
                sub.Columns.Add("Gname", typeof(string));
                sub.Columns.Add("GparentId", typeof(string));
                sub.Columns.Add("level", typeof(int));

                //looping through the result of the querry to load the result of the query to auxiliary datable and adding nodes to list of nodes 

                foreach (var x in queryRoot)
                {
                    DataRow rowSub = sub.NewRow();
                    rowSub["Gid"] = x.Gid;
                    rowSub["Gname"] = x.Gname;
                    rowSub["GparentId"] = x.GparentId;
                    rowSub["level"] = 0;
                    sub.Rows.Add(rowSub);

                    //creating NodeViewModel and adding to the list of NodeViewModel object list

                    nodesList.Add(new GoogleDriveTreeNodeViewModel
                    {
                        Id = x.Gid,
                        Name = x.Gname,
                        Expand = true,
                        Children =

                            new ObservableCollection<GoogleDriveTreeNodeViewModel>()
                    });

                }

                //adding Collection of NodeViewModel object with the root to TreeModel

                //Items = new ObservableCollection<GoogleDriveTreeNodeViewModel> { nodesList.Last()};
                Items = new ObservableCollection<GoogleDriveTreeNodeViewModel>();

                foreach (var item in nodesList)
                {
                    Items.Add(item);
                }

                int level = 0;


                NodesFactory(InputDT, sub, level);


            }
            else { MessageBox.Show("List of Direcitory is empty"); }
        }

        public void NodesFactory(DataTable InptDT, DataTable subDT, int level)
        {


            level = level + 1;


            var querryLevel =
            from itemsInputDT in InptDT.AsEnumerable()
            join itemSub in subDT.AsEnumerable() on itemsInputDT.Field<string>("GparentId") equals itemSub.Field<string>("Gid")
            where itemSub.Field<int>("level") == level - 1

            select new { Gid = itemsInputDT.Field<string>("Gid"), Gname = itemsInputDT.Field<string>("Gname"), GparentId = itemsInputDT.Field<string>("GparentId"), level = level };


            foreach (var x in querryLevel)
            {

                DataRow rowSub = subDT.NewRow();
                rowSub["Gid"] = x.Gid;
                rowSub["Gname"] = x.Gname;
                rowSub["GparentId"] = x.GparentId;
                rowSub["level"] = x.level;
                subDT.Rows.Add(rowSub);

                nodesList.Add(new GoogleDriveTreeNodeViewModel { Id = x.Gid, Name = x.Gname, Expand = true, Children = new ObservableCollection<GoogleDriveTreeNodeViewModel>() });

                nodesList.Find(gNode => gNode.Id == x.GparentId).Children.Add(nodesList.Last());

            }

            if (querryLevel.Count() > 0)
            { NodesFactory(InptDT, subDT, level); }


        }
    }
}
