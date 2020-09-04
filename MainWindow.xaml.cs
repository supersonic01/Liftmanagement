using Liftmanagement.Data;
using Liftmanagement.Helper;
using Liftmanagement.Models;
using Liftmanagement.View;
using Liftmanagement.ViewModels;
using Liftmanagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Liftmanagement
{
        /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        private CategoryViewModel categoryVM = new CategoryViewModel();

        public CategoryViewModel CategoryVM
        {
            get { return categoryVM; }
            set { categoryVM = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
            /*
                        DataGridTextColumn textColumn1 = new DataGridTextColumn();
                        textColumn1.Header = "Your header";
                        textColumn1.Binding = new Binding("CategoryVM.Categories.Name");
                        gridCategory.Columns.Add(textColumn1);
            */
            // gridCategory.ItemsSource = CategoryVM.Categories;
           // MyImage.DataContext = CategoryVM.Categories.First();
            ObservableCollection<Category> DataList = new ObservableCollection<Category>();

            DataList.Add(CategoryVM.Categories.First());
            MyDataGrid.ItemsSource = DataList;

            // MySQLDataAccess.TestConnection();
            // new CalendarQuickstart();
          //  new DriveQuickstart();
          //  new DirectoryQuickstart();

        }

        private void DataGridRow_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("KeyDown");
        }

        private void DataGridRow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("MouseDonw");
            // frameDetail.Content = new  CustomerView();
            frameDetail.Content = new GoogleDriveTreeView();
        }
    }
}
