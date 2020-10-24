using Liftmanagement.Data;
using Liftmanagement.Helper;
using Liftmanagement.Models;
using Liftmanagement.View;
using Liftmanagement.ViewModels;
using Liftmanagement.Views;
using Org.BouncyCastle.Asn1.Icao;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
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



            //  dgCategory.ItemsSource = CategoryVM.Categories;

            //MySQLDataAccess.CreateConnection();

           // MySQLDataAccess.CreateTables();



            //MySQLDataAccess.AddCustomer(TestData.GetCustomers().FirstOrDefault());
            //  MySQLDataAccess.AddMachineInformation(TestData.GetMachineInformations().FirstOrDefault());
            //MySQLDataAccess.AddMaintenanceAgreement(TestData.GetMaintenanceAgreements().FirstOrDefault());

            //MySQLDataAccess.GetCustomers();

            //Helper.Helper.GenerateInsert( typeof(MaintenanceAgreement));
            //  Helper.Helper.GenerateInsert(typeof(AdministratorCompany));
            
            //Helper.Helper.GenerateSelect(typeof(MaintenanceAgreement));
            //Helper.Helper.GenerateSelect(typeof(AdministratorCompany));
            //Helper.Helper.GenerateSelect(typeof(ContactPartner));

          //  Helper.Helper.GenerateUpdate(typeof(MaintenanceAgreement));
            //Helper.Helper.GenerateUpdate(typeof(AdministratorCompany));
            //Helper.Helper.GenerateUpdate(typeof(Location));


            //Helper.Helper.GetTabelValueHeaders(16);
            // new CalendarQuickstart();
            //  new DriveQuickstart();
            //  new DirectoryQuickstart();
            /*
            Binding b = new Binding("Temp2")
            {
                Source = this
            };
            listView1.SetBinding(ListView.ItemsSourceProperty, b);
            */

            // new TableGenerator();
       

            Binding binding = new Binding("CategoryVM.Categories")
            {
                Source = this
            };

            dgCategory.SetBinding(DataGrid.ItemsSourceProperty, binding);

            dgCategory.SelectedIndex = 0;

            frameToDos.Content = new ToDoView();
            frameAppointments.Content = new AppointmentView();
        }

        private void DataGridRow_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("KeyDown");
        }

        private void DataGridRow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("MouseDonw");
             frameDetail.Content = new  CustomerView();
            //frameDetail.Content = new GoogleDriveTreeView();
        }


        private void dgCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Category category = null;
            var dgCustomers = sender as DataGrid;
            if (dgCustomers != null)
                category = dgCategory.SelectedItem as Category;

            if (category == null)
            {
                return;

            }

            switch (category.MangementType)
            {
                case Helper.Helper.TTypeMangement.Customer:
                    frameDetail.Content = new CustomerView();
                    break;
                //TODO location
                //case Helper.Helper.TTypeMangement.Location:
                //    frameDetail.Content = new LocationView();
                //    break;
                case Helper.Helper.TTypeMangement.MaintenanceAgreement:
                    frameDetail.Content = new MaintenanceAgreementView();
                    break;
                case Helper.Helper.TTypeMangement.EmergencyAgreement:
                    break;
                case Helper.Helper.TTypeMangement.MachineInformation:
                    frameDetail.Content = new MachineInformationView();
                    break;
                case Helper.Helper.TTypeMangement.Task:
                    break;
                case Helper.Helper.TTypeMangement.Profil:
                    break;
                case Helper.Helper.TTypeMangement.Update:
                    break;
                case Helper.Helper.TTypeMangement.Managment:
                    //frameDetail.Content = new Test();
                    //frameDetail.Content = new MasterDataInfoView();
                     frameDetail.Content = new ManagementView();

                    //var window = new Window
                    //{
                    //    Title = "Suche",
                    //    Content = new RecordingView()
                    //};
                    //window.Show();

                    //var location = TestData.GetLocations().FirstOrDefault();
                    ////
                    //frameDetail.Content = new LocationDetailView(location);
                   // frameDetail.Content = new BusyIndicatorView();

                    break;
                default:
                    break;
            }

        }
    }
}
