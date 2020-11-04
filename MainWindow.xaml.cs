using Liftmanagement.Models;
using Liftmanagement.View;
using Liftmanagement.ViewModels;
using Liftmanagement.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Liftmanagement.Data;

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

            //Helper.Helper.GenerateInsert(typeof(MaintenanceAgreementContent));
            //Helper.Helper.GenerateInsert(typeof(OtherInformation));
            // Helper.Helper.GenerateInsert(typeof(Record));

            //Helper.Helper.GenerateSelect(typeof(MaintenanceAgreementContent));
            //Helper.Helper.GenerateSelect(typeof(OtherInformation));
            //Helper.Helper.GenerateSelect(typeof(Record));

            //Helper.Helper.GenerateUpdate(typeof(MaintenanceAgreementContent));
            //Helper.Helper.GenerateUpdate(typeof(OtherInformation));
            //Helper.Helper.GenerateUpdate(typeof(Record));


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


            //  new CalendarQuickstart().AddEvent(DateTime.Now, DateTime.Now, "Hallo", "Hallo2");



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
                     frameDetail.Content = new ManagementView(dgCategory);

                    //var window = new Window
                    //{
                    //    Title = "Suche",
                    //    Content = new RecordView()
                    //};
                    //window.Show();

                    //var location = TestData.GetLocations().FirstOrDefault();
                    ////
                    //frameDetail.Content = new LocationDetailView(location);
                   // frameDetail.Content = new BusyIndicatorView();

                    break;
                case Helper.Helper.TTypeMangement.Report:
                    frameDetail.Content = new ReportView();
                    break;
                default:
                    break;
            }

        }
    }
}
