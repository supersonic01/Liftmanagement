
using Liftmanagement.Helper;
using Liftmanagement.Models;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Liftmanagement.Data
{
    public static class MySQLDataAccess
    {

        private static MySqlConnection databaseConnection = null;

        public static void CreateConnection()
        {
            string connectionString = GetConnectionString();
            databaseConnection = new MySqlConnection(connectionString);
        }

        public static MySqlConnection GetConnection()
        {
            string connectionString = GetConnectionString();
            return  new MySqlConnection(connectionString);
        }

        public static void CreateTables()
        {
            if (databaseConnection == null)
            {
                CreateConnection();
            }

            var tblGenerator = new TableGenerator();
            MySqlCommand Create_table = new MySqlCommand(tblGenerator.CreatScript.ToString(), databaseConnection);
            databaseConnection.Open();
            Create_table.ExecuteNonQuery();
            databaseConnection.Close();
        }

        public static SQLQueryResult<Customer> AddCustomer(Customer customer)
        {
            //TOTO do in trasaction

            if (databaseConnection == null)
            {
                CreateConnection();
            }

            string query = "INSERT INTO CUSTOMER(CompanyName,Address,Postcode,City,Selected,AdditionalInfo,GoogleDriveFolderName,GoogleDriveLink,CreatedPersonName,ModifiedPersonName,ReadOnly,UsedBy)";
            string values = "VALUE('" + customer.CompanyName + "','" + customer.Address + "','" + customer.Postcode + "','" + customer.City + "'," + customer.Selected + ",'" + customer.AdditionalInfo + "','" + customer.GoogleDriveFolderName + "','" + customer.GoogleDriveLink + "','" + customer.CreatedPersonName + "','" + customer.ModifiedPersonName + "'," + customer.ReadOnly + ",'" + customer.UsedBy + "')";
            query = query + values;


            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            databaseConnection.Open();
            int records = execQuery.ExecuteNonQuery();
            long id = execQuery.LastInsertedId;
            var result = new SQLQueryResult<Customer>(records, id);

            customer.ContactPerson.ForeignKey = id;
            customer.ContactPerson.CustomerId = id;
            customer.ContactPerson.ForeignKeyType = Helper.Helper.ClassTypeForeignKeyTypeMapper[typeof(Customer)];
            var contactResult = AddContactPartner(customer.ContactPerson);
            //  result.AddSQLSubQueryResult(contactResult, result);

            customer.Administrator.CustomerId = id;
            var adminResult = AddAdministratorCompany(customer.Administrator);
            // result.AddSQLSubQueryResult(adminResult, result);

            foreach (var contactPerson in customer.Administrator.ContactPersons)
            {
                if (contactPerson.Id < 0)
                {
                    contactPerson.ForeignKey = adminResult.Id;
                    contactPerson.CustomerId = customer.Administrator.CustomerId;
                    contactPerson.ForeignKeyType =
                        Helper.Helper.ClassTypeForeignKeyTypeMapper[typeof(AdministratorCompany)];
                    var res = AddContactPartner(contactPerson);
                    //result.AddSQLSubQueryResult(res, adminResult);
                }
            }

            databaseConnection.Close();

            return result;
        }

        private static SQLQueryResult<ContactPartner> AddContactPartner(ContactPartner contactpartner)
        {
            string query = "INSERT INTO CONTACTPARTNER(CustomerId,ForeignKey,ForeignKeyType,Name,PhoneWork,Mobile,EMail,ContactByDefect,CreatedPersonName,ModifiedPersonName,ReadOnly,UsedBy)";
            string values = "VALUE(" + contactpartner.CustomerId + "," + contactpartner.ForeignKey + "," + contactpartner.ForeignKeyType + ",'" + contactpartner.Name + "','" + contactpartner.PhoneWork + "','" + contactpartner.Mobile + "','" + contactpartner.EMail + "'," + contactpartner.ContactByDefect + ",'" + contactpartner.CreatedPersonName + "','" + contactpartner.ModifiedPersonName + "'," + contactpartner.ReadOnly + ",'" + contactpartner.UsedBy + "')";
            query = query + values;


            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            int records = execQuery.ExecuteNonQuery();
            long id = execQuery.LastInsertedId;
            return new SQLQueryResult<ContactPartner>(records, id);
        }

        public static SQLQueryResult<Location> AddLocation(Location location)
        {
            if (databaseConnection == null)
            {
                CreateConnection();
            }

            string query = "INSERT INTO LOCATION(CustomerId,Address,Postcode,City,Selected,AdditionalInfo,GoogleDriveFolderName,GoogleDriveLink,CreatedPersonName,ModifiedPersonName,ReadOnly,UsedBy)";
            string values = "VALUE(" + location.CustomerId + ",'" + location.Address + "','" + location.Postcode + "','" + location.City + "'," + location.Selected + ",'" + location.AdditionalInfo + "','" + location.GoogleDriveFolderName + "','" + location.GoogleDriveLink + "','" + location.CreatedPersonName + "','" + location.ModifiedPersonName + "'," + location.ReadOnly + ",'" + location.UsedBy + "')";
            query = query + values;


            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            databaseConnection.Open();

            int records = execQuery.ExecuteNonQuery();
            long id = execQuery.LastInsertedId;
            var result = new SQLQueryResult<Location>(records, id);

            location.ContactPerson.ForeignKey = id;
            location.ContactPerson.CustomerId = location.CustomerId;
            location.ContactPerson.ForeignKeyType = Helper.Helper.ClassTypeForeignKeyTypeMapper[typeof(Location)];
            var contactResult = AddContactPartner(location.ContactPerson);
            // result.AddSQLSubQueryResult(contactResult, result);

            databaseConnection.Close();

            return result;
        }

        private static SQLQueryResult<AdministratorCompany> AddAdministratorCompany(AdministratorCompany administratorcompany)
        {
            string query = "INSERT INTO ADMINISTRATORCOMPANY(CustomerId,Name,CreatedPersonName,ModifiedPersonName,ReadOnly,UsedBy)";
            string values = "VALUE(" + administratorcompany.CustomerId + ",'" + administratorcompany.Name + "','" + administratorcompany.CreatedPersonName + "','" + administratorcompany.ModifiedPersonName + "'," + administratorcompany.ReadOnly + ",'" + administratorcompany.UsedBy + "')";
            query = query + values;


            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            int records = execQuery.ExecuteNonQuery();
            long id = execQuery.LastInsertedId;

            return new SQLQueryResult<AdministratorCompany>(records, id);
        }

        public static SQLQueryResult<MachineInformation> AddMachineInformation(MachineInformation machineinformation)
        {
            if (databaseConnection == null)
            {
                CreateConnection();
            }

            string query = "INSERT INTO MACHINEINFORMATION (LocationId,CustomerId,Name,YearOfConstruction,SerialNumber,HoldingPositions,Entrances,Payload,Description,GoogleDriveFolderName,GoogleDriveLink,CreatedPersonName,ModifiedPersonName,ReadOnly,UsedBy,Deleted)";
            string values = "VALUE(" + machineinformation.LocationId + "," + machineinformation.CustomerId + ",'" + machineinformation.Name + "','" + machineinformation.YearOfConstruction.ToString("yyyy-MM-dd") + "','" + machineinformation.SerialNumber + "'," + machineinformation.HoldingPositions + "," + machineinformation.Entrances + "," + machineinformation.Payload + ",'" + machineinformation.Description + "','" + machineinformation.GoogleDriveFolderName + "','" + machineinformation.GoogleDriveLink + "','" + machineinformation.CreatedPersonName + "','" + machineinformation.ModifiedPersonName + "'," + machineinformation.ReadOnly + ",'" + machineinformation.UsedBy + "'," + machineinformation.Deleted + ")";
            query = query + values;


            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            databaseConnection.Open();
            int records = execQuery.ExecuteNonQuery();
            long id = execQuery.LastInsertedId;
            var result = new SQLQueryResult<MachineInformation>(records, id);

            machineinformation.ContactPerson.ForeignKey = id;
            machineinformation.ContactPerson.ForeignKeyType = Helper.Helper.ClassTypeForeignKeyTypeMapper[typeof(MachineInformation)];
            var contactResult = AddContactPartner(machineinformation.ContactPerson);

            databaseConnection.Close();
            return result;
        }


        public static SQLQueryResult<MaintenanceAgreement> AddMaintenanceAgreement(MaintenanceAgreement maintenanceagreement)
        {
            var dbConnection = GetConnection();

            string query = "INSERT INTO MAINTENANCEAGREEMENT(LocationId,CustomerId,MachineInformationId,Duration,CanBeCancelled,AgreementCancelledBy,NoticeOfPeriod,AgreementDate,MaintenanceType,AdditionalInfo,NotificationTime,NotificationUnit,GoogleCalendarEventId,GoogleDriveFolderName,GoogleDriveLink,CreatedPersonName,ModifiedPersonName,ReadOnly,UsedBy,Deleted)";
            string values = "VALUE(" + maintenanceagreement.LocationId + "," + maintenanceagreement.CustomerId + "," + maintenanceagreement.MachineInformationId + ",'" + maintenanceagreement.Duration.ToString("yyyy-MM-dd") + "','" + maintenanceagreement.CanBeCancelled + "','" + maintenanceagreement.AgreementCancelledBy + "'," + maintenanceagreement.NoticeOfPeriod + ",'" + maintenanceagreement.AgreementDate.ToString("yyyy-MM-dd") + "','" + maintenanceagreement.MaintenanceType + "','" + maintenanceagreement.AdditionalInfo + "'," + maintenanceagreement.NotificationTime + "," + (int)maintenanceagreement.NotificationUnit + ",'" + maintenanceagreement.GoogleCalendarEventId + "','" + maintenanceagreement.GoogleDriveFolderName + "','" + maintenanceagreement.GoogleDriveLink + "','" + maintenanceagreement.CreatedPersonName + "','" + maintenanceagreement.ModifiedPersonName + "'," + maintenanceagreement.ReadOnly + ",'" + maintenanceagreement.UsedBy + "'," + maintenanceagreement.Deleted + ")";
            query = query + values;
            
            MySqlCommand execQuery = new MySqlCommand(query, dbConnection);

            dbConnection.Open();
            int records = execQuery.ExecuteNonQuery();
            long id = execQuery.LastInsertedId;

            dbConnection.Close();

            return new SQLQueryResult<MaintenanceAgreement>(records, id);
        }

        public static List<Customer> GetCustomers()
        {
            string query = "SELECT * FROM CUSTOMER";
            return GetCustomers(query);
        }

        private static List<Customer> GetCustomers(string query)
        {
            List<Customer> customers = new List<Customer>();

            SelectItems(query, reader =>
            {
                Customer customer = new Customer();
                customer.CompanyName = reader.GetString("CompanyName");
                customer.Address = reader.GetString("Address");
                customer.Postcode = reader.GetString("Postcode");
                customer.City = reader.GetString("City");
                customer.Selected = reader.GetBoolean("Selected");
                customer.AdditionalInfo = reader.GetString("AdditionalInfo");
                customer.GoogleDriveFolderName = reader.GetString("GoogleDriveFolderName");
                customer.GoogleDriveLink = reader.GetString("GoogleDriveLink");
                customer.Id = reader.GetInt64("Id");
                customer.CreatedDate = reader.GetDateTime("CreatedDate");
                customer.ModifiedDate = reader.GetDateTime("ModifiedDate");
                customer.CreatedPersonName = reader.GetString("CreatedPersonName");
                customer.ModifiedPersonName = reader.GetString("ModifiedPersonName");
                customer.ReadOnly = reader.GetBoolean("ReadOnly");
                customer.UsedBy = reader.GetString("UsedBy");
                customers.Add(customer);
            });

            foreach (var customer in customers)
            {
                query = "SELECT * FROM ADMINISTRATORCOMPANY WHERE CUSTOMERID = " + customer.Id;

                SelectItems(query, reader =>
                {
                    AdministratorCompany administratorcompany = new AdministratorCompany();
                    administratorcompany.CustomerId = reader.GetInt64("CustomerId");
                    administratorcompany.Name = reader.GetString("Name");
                    administratorcompany.Id = reader.GetInt64("Id");
                    administratorcompany.CreatedDate = reader.GetDateTime("CreatedDate");
                    administratorcompany.ModifiedDate = reader.GetDateTime("ModifiedDate");
                    administratorcompany.CreatedPersonName = reader.GetString("CreatedPersonName");
                    administratorcompany.ModifiedPersonName = reader.GetString("ModifiedPersonName");
                    administratorcompany.ReadOnly = reader.GetBoolean("ReadOnly");
                    administratorcompany.UsedBy = reader.GetString("UsedBy");
                    customer.Administrator = administratorcompany;
                });

                customer.ContactPerson = GetContactPartners(customer.Id,
                    Helper.Helper.ClassTypeForeignKeyTypeMapper[typeof(Customer)]).FirstOrDefault();

                customer.Administrator.ContactPersons = GetContactPartners(customer.Administrator.Id, Helper.Helper.ClassTypeForeignKeyTypeMapper[typeof(AdministratorCompany)]);

            }

            return customers;

        }

        private static List<Location> GetLocations(string query)
        {
            List<Location> locations = new List<Location>();

            SelectItems(query, reader =>
            {
                Location location = new Location();
                location.CustomerId = reader.GetInt64("CustomerId");
                location.Address = reader.GetString("Address");
                location.Postcode = reader.GetString("Postcode");
                location.City = reader.GetString("City");
                location.Selected = reader.GetBoolean("Selected");
                location.AdditionalInfo = reader.GetString("AdditionalInfo");
                location.GoogleDriveFolderName = reader.GetString("GoogleDriveFolderName");
                location.GoogleDriveLink = reader.GetString("GoogleDriveLink");
                location.Id = reader.GetInt64("Id");
                location.CreatedDate = reader.GetDateTime("CreatedDate");
                location.ModifiedDate = reader.GetDateTime("ModifiedDate");
                location.CreatedPersonName = reader.GetString("CreatedPersonName");
                location.ModifiedPersonName = reader.GetString("ModifiedPersonName");
                location.ReadOnly = reader.GetBoolean("ReadOnly");
                location.UsedBy = reader.GetString("UsedBy");
                locations.Add(location);
            });

            foreach (var location in locations)
            {
                location.ContactPerson = GetContactPartners(location.Id,
                    Helper.Helper.ClassTypeForeignKeyTypeMapper[typeof(Location)]).FirstOrDefault();
            }

            return locations;

        }

        public static SQLQueryResult<Customer> GetCustomerForEdit(long id)
        {
            return GetRecordForEdit(id, query => GetCustomers(query));
        }

        private static SQLQueryResult<T> GetRecordForEdit<T>(long id, Func<string, List<T>> GetRecords) where T : BaseDatabaseField
        {
            // TODO check user, if is the same user, editing is possilbe

            var classname = typeof(T).Name.ToUpper();

            string query = "SELECT * FROM " + classname + " WHERE ID= " + id;
            var result = new SQLQueryResult<T>(0, id);
            result.DBRecords = GetRecords(query);

            var item = result.DBRecords.FirstOrDefault();

            if (item.ReadOnly)
            {
                result.IsReadOnly = true;
                result.CurrentlyUsedBy = item.UsedBy;
            }
            else
            {
                var updateQuery = "UPDATE " + classname + " SET READONLY = 1, USEDBY = '" + Helper.Helper.GetUsername() + "' WHERE ID = " + id;

                if (databaseConnection == null)
                {
                    CreateConnection();
                }

                MySqlCommand execQuery = new MySqlCommand(updateQuery, databaseConnection);

                databaseConnection.Open();
                int records = execQuery.ExecuteNonQuery();

                databaseConnection.Close();

                result.DBRecords = GetRecords(query);
            }

            return result;
        }


        public static SQLQueryResult<Location> GetLocationForEdit(long id)
        {
            return GetRecordForEdit(id, query => GetLocations(query));

            //// TODO check user, if is the same user, editing is possilbe
        }

        public static SQLQueryResult<Location> ForceToEditLocation(long id)
        {
            return ForceToEdit<Location>(id);
        }
        public static SQLQueryResult<Customer> ForceToEditCustomer(long id)
        {
            return ForceToEdit<Customer>(id);
        }

        public static SQLQueryResult<Customer> ReleaseEditingCustomer(long id)
        {
            return ReleaseEditing<Customer>(id);
        }

        public static SQLQueryResult<Location> ReleaseEditingLocation(long id)
        {
            return ReleaseEditing<Location>(id);
        }

        private static SQLQueryResult<T> ForceToEdit<T>(long id)
            where T : BaseDatabaseField
        {
            var classname = typeof(T).Name.ToUpper();
            var updateQuery = "UPDATE " + classname + " SET READONLY = 1, USEDBY = '" + Helper.Helper.GetUsername() + "' WHERE ID = " + id;

            return SetEditing<T>(id, updateQuery);
        }

        private static SQLQueryResult<T> ReleaseEditing<T>(long id)
            where T : BaseDatabaseField
        {
            var classname = typeof(T).Name.ToUpper();
            var updateQuery = "UPDATE " + classname + " SET READONLY = 0, USEDBY = '' WHERE ID = " + id;

            return SetEditing<T>(id, updateQuery);
        }

        private static SQLQueryResult<T> SetEditing<T>(long id, string updateQuery)
            where T : BaseDatabaseField
        {
            if (databaseConnection == null)
            {
                CreateConnection();
            }

            MySqlCommand execQuery = new MySqlCommand(updateQuery, databaseConnection);

            databaseConnection.Open();
            int records = execQuery.ExecuteNonQuery();
            databaseConnection.Close();

            return new SQLQueryResult<T>(records, id);
        }


        private static List<ContactPartner> GetContactPartners(long foreignkey, int foreignkeytype)
        {
            string query;
            List<ContactPartner> contactpartners = new List<ContactPartner>();
            query = "SELECT * FROM CONTACTPARTNER WHERE FOREIGNKEY = " + foreignkey + " AND FOREIGNKEYTYPE = " + foreignkeytype;

            SelectItems(query, reader =>
            {
                ContactPartner contactpartner = new ContactPartner();
                contactpartner.CustomerId = reader.GetInt64("CustomerId");
                contactpartner.ForeignKey = reader.GetInt64("ForeignKey");
                contactpartner.ForeignKeyType = reader.GetInt32("ForeignKeyType");
                contactpartner.Name = reader.GetString("Name");
                contactpartner.PhoneWork = reader.GetString("PhoneWork");
                contactpartner.Mobile = reader.GetString("Mobile");
                contactpartner.EMail = reader.GetString("EMail");
                contactpartner.ContactByDefect = reader.GetBoolean("ContactByDefect");
                contactpartner.Id = reader.GetInt64("Id");
                contactpartner.CreatedDate = reader.GetDateTime("CreatedDate");
                contactpartner.ModifiedDate = reader.GetDateTime("ModifiedDate");
                contactpartner.CreatedPersonName = reader.GetString("CreatedPersonName");
                contactpartner.ModifiedPersonName = reader.GetString("ModifiedPersonName");
                contactpartner.ReadOnly = reader.GetBoolean("ReadOnly");
                contactpartner.UsedBy = reader.GetString("UsedBy");
                contactpartners.Add(contactpartner);
            });
            return contactpartners;
        }

        public static List<Location> GetLocations()
        {
            string query = "SELECT * FROM LOCATION";
            return GetLocations(query);
        }
        public static List<Location> GetLocations(Customer customer)
        {
            string query = "SELECT * FROM LOCATION WHERE CUSTOMERID = " + customer.Id;
            return GetLocations(query);
        }

        public static List<Location> GetLocationsByCustomer(long id)
        {
            string query = "SELECT * FROM LOCATION WHERE CUSTOMERID = " + id;
            return GetLocations(query);
        }

        public static List<Location> GetLocations(long id)
        {
            string query = "SELECT * FROM LOCATION WHERE ID = " + id;
            return GetLocations(query);
        }

        public static List<MaintenanceAgreement> GetMaintenanceAgreements()
        {
            string query = "SELECT * FROM MAINTENANCEAGREEMENT";
            return GetMaintenanceAgreements(query);
        }

        private static List<MaintenanceAgreement> GetMaintenanceAgreements( string query)
        {
            List<MaintenanceAgreement> maintenanceAgreements = new List<MaintenanceAgreement>();

            SelectItems(query, reader =>
            {
                MaintenanceAgreement maintenanceagreement = new MaintenanceAgreement();
                maintenanceagreement.LocationId = reader.GetInt64("LocationId");
                maintenanceagreement.CustomerId = reader.GetInt64("CustomerId");
                maintenanceagreement.MachineInformationId = reader.GetInt64("MachineInformationId");
                maintenanceagreement.Duration = reader.GetDateTime("Duration");
                maintenanceagreement.CanBeCancelled = reader.GetString("CanBeCancelled");
                maintenanceagreement.AgreementCancelledBy = reader.GetString("AgreementCancelledBy");
                maintenanceagreement.NoticeOfPeriod = reader.GetInt32("NoticeOfPeriod");
                maintenanceagreement.AgreementDate = reader.GetDateTime("AgreementDate");
                maintenanceagreement.MaintenanceType = reader.GetString("MaintenanceType");
                maintenanceagreement.AdditionalInfo = reader.GetString("AdditionalInfo");
                maintenanceagreement.NotificationTime = reader.GetInt32("NotificationTime");
                maintenanceagreement.NotificationUnit = (Helper.Helper.NotificationUnitType) reader.GetInt32("NotificationUnit");
                maintenanceagreement.GoogleCalendarEventId = reader.GetString("GoogleCalendarEventId");
                maintenanceagreement.GoogleDriveFolderName = reader.GetString("GoogleDriveFolderName");
                maintenanceagreement.GoogleDriveLink = reader.GetString("GoogleDriveLink");
                maintenanceagreement.Id = reader.GetInt64("Id");
                maintenanceagreement.CreatedDate = reader.GetDateTime("CreatedDate");
                maintenanceagreement.ModifiedDate = reader.GetDateTime("ModifiedDate");
                maintenanceagreement.CreatedPersonName = reader.GetString("CreatedPersonName");
                maintenanceagreement.ModifiedPersonName = reader.GetString("ModifiedPersonName");
                maintenanceagreement.ReadOnly = reader.GetBoolean("ReadOnly");
                maintenanceagreement.UsedBy = reader.GetString("UsedBy");
                maintenanceagreement.Deleted = reader.GetBoolean("Deleted");
                maintenanceAgreements.Add(maintenanceagreement);
            });

            return maintenanceAgreements;
        }

        public static SQLQueryResult<Customer> UpdateCustomer(Customer customer)
        {
            //TOTO do in trasaction
            //Check Timestemp if needed

            if (databaseConnection == null)
            {
                CreateConnection();
            }

            customer.ModifiedPersonName = Helper.Helper.GetPersonName();
            string query = "UPDATE CUSTOMER SET CompanyName = '" + customer.CompanyName + "',Address = '" + customer.Address + "',Postcode = '" + customer.Postcode + "',City = '" + customer.City + "',Selected = " + customer.Selected + ",AdditionalInfo = '" + customer.AdditionalInfo + "',GoogleDriveFolderName = '" + customer.GoogleDriveFolderName + "',GoogleDriveLink = '" + customer.GoogleDriveLink + "',CreatedPersonName = '" + customer.CreatedPersonName + "',ModifiedPersonName = '" + customer.ModifiedPersonName + "',ReadOnly = " + customer.ReadOnly + ",UsedBy = '" + customer.UsedBy + "' WHERE ID = " + customer.Id;

            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            databaseConnection.Open();
            int records = execQuery.ExecuteNonQuery();
            long id = customer.Id;
            var result = new SQLQueryResult<Customer>(records, id);

            UpdateContactPartner(customer.ContactPerson);

            UpdateAdministratorCompany(customer.Administrator);

            foreach (var contactPerson in customer.Administrator.ContactPersons)
            {
                contactPerson.ForeignKey = customer.Administrator.Id;
                contactPerson.CustomerId = customer.Administrator.CustomerId;
                contactPerson.ForeignKeyType =
                    Helper.Helper.ClassTypeForeignKeyTypeMapper[typeof(AdministratorCompany)];

                if (contactPerson.Id < 0)
                {
                    AddContactPartner(contactPerson);
                }
                else
                {
                    UpdateContactPartner(contactPerson);
                }
            }

            foreach (var contactPerson in customer.Administrator.DeletedContactPersons)
            {
                query = "DELETE FROM CONTACTPARTNER WHERE ID = " + contactPerson.Id;
                execQuery = new MySqlCommand(query, databaseConnection);
                execQuery.ExecuteNonQuery();
            }

            databaseConnection.Close();

            return result;
        }

        public static SQLQueryResult<Customer> MarkForDeleteCustomer(Customer customer)
        {
            //TOTO do in trasaction
            //Check Timestemp if needed

            if (databaseConnection == null)
            {
                CreateConnection();
            }

            customer.ModifiedPersonName = Helper.Helper.GetPersonName();
            string query = "UPDATE CUSTOMER SET Selected = " + customer.Selected + ",ModifiedPersonName = '" + customer.ModifiedPersonName + "' WHERE ID = " + customer.Id;

            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            databaseConnection.Open();
            int records = execQuery.ExecuteNonQuery();
            long id = customer.Id;
            var result = new SQLQueryResult<Customer>(records, id);

            Task.Factory.StartNew(() =>
            {
                MarkForDeleteContactPartner(customer.ContactPerson);

                MarkForDeleteAdministratorCompany(customer.Administrator);

                foreach (var contactPerson in customer.Administrator.ContactPersons)
                {
                    MarkForDeleteContactPartner(contactPerson);
                }
            }).ContinueWith((task) =>
            {
                databaseConnection.Close();
            });

            return result;
        }

        private static SQLQueryResult<ContactPartner> MarkForDeleteContactPartner(ContactPartner contactpartner)
        {
            contactpartner.ModifiedPersonName = Helper.Helper.GetPersonName();
            string query = "UPDATE CONTACTPARTNER SET DELETED = " + contactpartner.Deleted + ",MODIFIEDPERSONNAME = '" + contactpartner.ModifiedPersonName + "' WHERE CustomerId = " + contactpartner.CustomerId;

            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            int records = execQuery.ExecuteNonQuery();
            return new SQLQueryResult<ContactPartner>(records, contactpartner.Id);
        }

        public static SQLQueryResult<Location> MarkForDeleteLocation(Location location)
        {
            if (databaseConnection == null)
            {
                CreateConnection();
            }

            location.ModifiedPersonName = Helper.Helper.GetPersonName();
            string query = "UPDATE LOCATION SET DELETED = " + location.Deleted + ",MODIFIEDPERSONNAME = '" + location.ModifiedPersonName + "' WHERE CustomerId = " + location.CustomerId;

            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            databaseConnection.Open();

            int records = execQuery.ExecuteNonQuery();
            var result = new SQLQueryResult<Location>(records, location.Id);

            var contactResult = MarkForDeleteContactPartner(location.ContactPerson);

            databaseConnection.Close();

            return result;
        }

        private static SQLQueryResult<AdministratorCompany> MarkForDeleteAdministratorCompany(AdministratorCompany administratorcompany)
        {
            administratorcompany.ModifiedPersonName = Helper.Helper.GetPersonName();
            string query = "UPDATE ADMINISTRATORCOMPANY SET DELETED = " + administratorcompany.Deleted + ",MODIFIEDPERSONNAME = '" + administratorcompany.ModifiedPersonName + "' WHERE CustomerId = " + administratorcompany.CustomerId;

            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            int records = execQuery.ExecuteNonQuery();

            return new SQLQueryResult<AdministratorCompany>(records, administratorcompany.Id);
        }

        private static SQLQueryResult<ContactPartner> UpdateContactPartner(ContactPartner contactpartner)
        {
            contactpartner.ModifiedPersonName = Helper.Helper.GetPersonName();
            string query = "UPDATE CONTACTPARTNER SET CustomerId = " + contactpartner.CustomerId + ",ForeignKey = " + contactpartner.ForeignKey + ",ForeignKeyType = " + contactpartner.ForeignKeyType + ",Name = '" + contactpartner.Name + "',PhoneWork = '" + contactpartner.PhoneWork + "',Mobile = '" + contactpartner.Mobile + "',EMail = '" + contactpartner.EMail + "',ContactByDefect = " + contactpartner.ContactByDefect + ",CreatedPersonName = '" + contactpartner.CreatedPersonName + "',ModifiedPersonName = '" + contactpartner.ModifiedPersonName + "',ReadOnly = " + contactpartner.ReadOnly + ",UsedBy = '" + contactpartner.UsedBy + "' WHERE ID = " + contactpartner.Id;

            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            int records = execQuery.ExecuteNonQuery();
            return new SQLQueryResult<ContactPartner>(records, contactpartner.Id);
        }

        public static SQLQueryResult<Location> UpdateLocation(Location location)
        {
            if (databaseConnection == null)
            {
                CreateConnection();
            }

            location.ModifiedPersonName = Helper.Helper.GetPersonName();
            string query = "UPDATE LOCATION SET CustomerId = " + location.CustomerId + ",Address = '" + location.Address + "',Postcode = '" + location.Postcode + "',City = '" + location.City + "',Selected = " + location.Selected + ",AdditionalInfo = '" + location.AdditionalInfo + "',GoogleDriveFolderName = '" + location.GoogleDriveFolderName + "',GoogleDriveLink = '" + location.GoogleDriveLink + "',CreatedPersonName = '" + location.CreatedPersonName + "',ModifiedPersonName = '" + location.ModifiedPersonName + "',ReadOnly = " + location.ReadOnly + ",UsedBy = '" + location.UsedBy + "' WHERE ID = " + location.Id;

            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            databaseConnection.Open();

            int records = execQuery.ExecuteNonQuery();
            var result = new SQLQueryResult<Location>(records, location.Id);

            location.ContactPerson.ForeignKey = location.Id;
            location.ContactPerson.CustomerId = location.CustomerId;
            location.ContactPerson.ForeignKeyType = Helper.Helper.ClassTypeForeignKeyTypeMapper[typeof(Location)];
            var contactResult = UpdateContactPartner(location.ContactPerson);

            databaseConnection.Close();

            return result;
        }

        private static SQLQueryResult<AdministratorCompany> UpdateAdministratorCompany(AdministratorCompany administratorcompany)
        {
            administratorcompany.ModifiedPersonName = Helper.Helper.GetPersonName();
            string query = "UPDATE ADMINISTRATORCOMPANY SET CustomerId = " + administratorcompany.CustomerId + ",Name = '" + administratorcompany.Name + "',CreatedPersonName = '" + administratorcompany.CreatedPersonName + "',ModifiedPersonName = '" + administratorcompany.ModifiedPersonName + "',ReadOnly = " + administratorcompany.ReadOnly + ",UsedBy = '" + administratorcompany.UsedBy + "' WHERE ID = " + administratorcompany.Id;

            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            int records = execQuery.ExecuteNonQuery();

            return new SQLQueryResult<AdministratorCompany>(records, administratorcompany.Id);
        }



        private static void SelectItems(string query, Action<MySqlDataReader> getRecords)
        {
            var dbConnection = GetConnection();

            MySqlCommand commandDatabase = new MySqlCommand(query, dbConnection);
            commandDatabase.CommandTimeout = 60;

            MySqlDataReader reader;

            try
            {
                // Open the database
                dbConnection.Open();

                // Execute the query
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        getRecords(reader);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.
                Console.WriteLine(ex);
                throw;
            }
            finally
            {

                // Finally close the connection
                dbConnection.Close();
            }

        }

        public static void CreateConnection1()
        {
            //https://dbadmin.hosteurope.de/phpmyadmin/sql.php?server=1&db=db1096358-liftmanagement&table=TestDBRex&pos=0
            //DROP TABLE `AdministratorCompany`, `Customer`;

            string connectionString = GetConnectionString();
            // databaseConnection = new MySqlConnection(connectionString);



            string query = "SELECT * FROM TestDBRex";

            // Prepare the connection
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;




            //  MySqlCommand Create_table = new MySqlCommand("CREATE TABLE CustomerID BIGINT,	 CustomerId BIGINT,	 CompanyName NVARCHAR(500),	 GoogleDriveFolderName VARCHAR(500),	 GoogleDriveLink NVARCHAR(500),	 Administrator BIGINT,	 Address NVARCHAR(500),	 Postcode NVARCHAR(500),	 City NVARCHAR(500),	 Selected BIT,	 AdditionalInfo NVARCHAR(500),	 ContactPerson BIGINT,	FullName NVARCHAR(500))", databaseConnection);
            var tblGenerator = new TableGenerator();


            //foreach (var script in tblGenerator.CreatScripts)
            //{
            //    MySqlCommand Create_table = new MySqlCommand(script.ToString(), databaseConnection);
            //    Create_table.ExecuteNonQuery();
            //}

            MySqlCommand Create_table = new MySqlCommand(tblGenerator.CreatScript.ToString(), databaseConnection);
            databaseConnection.Open();
            Create_table.ExecuteNonQuery();
            databaseConnection.Close();






            MySqlDataReader reader;

            // Let's do it !
            try
            {
                // Open the database
                databaseConnection.Open();

                // Execute the query
                reader = commandDatabase.ExecuteReader();

                // All succesfully executed, now do something

                // IMPORTANT : 
                // If your query returns result, use the following processor :

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // As our database, the array will contain : ID 0, FIRST_NAME 1,LAST_NAME 2, ADDRESS 3
                        // Do something with every received database ROW
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };

                        Console.WriteLine(row);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }

                // Finally close the connection
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.

            }
        }

        private static string GetConnectionString()
        {
            var server = "wp1096358.server-he.de";
            var database = "db1096358-liftmanagement";
            var uid = "db1096358-lift";
            var password = "Elfe100Lift:01#";
            string connectionString;
            //        connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            //database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                               database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            return connectionString;
        }

        public static SQLQueryResult<MachineInformation> UpdateMachineInformation(MachineInformation machineinformation)
        {
            if (databaseConnection == null)
            {
                CreateConnection();
            }

            machineinformation.ModifiedPersonName = Helper.Helper.GetPersonName();
            string query = "UPDATE MACHINEINFORMATION SET LocationId = " + machineinformation.LocationId + ",CustomerId = " + machineinformation.CustomerId + ",Name = '" + machineinformation.Name + "',YearOfConstruction = '" + machineinformation.YearOfConstruction + "',SerialNumber = '" + machineinformation.SerialNumber + "',HoldingPositions = " + machineinformation.HoldingPositions + ",Entrances = " + machineinformation.Entrances + ",Payload = " + machineinformation.Payload + ",Description = '" + machineinformation.Description + "',GoogleDriveFolderName = '" + machineinformation.GoogleDriveFolderName + "',GoogleDriveLink = '" + machineinformation.GoogleDriveLink + "',CreatedPersonName = '" + machineinformation.CreatedPersonName + "',ModifiedPersonName = '" + machineinformation.ModifiedPersonName + "',ReadOnly = " + machineinformation.ReadOnly + ",UsedBy = '" + machineinformation.UsedBy + "',Deleted = " + machineinformation.Deleted + " WHERE ID = "+machineinformation.Id;
            
            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            databaseConnection.Open();
            int records = execQuery.ExecuteNonQuery();
            var result = new SQLQueryResult<MachineInformation>(records, machineinformation.Id);

            UpdateContactPartner(machineinformation.ContactPerson);

            databaseConnection.Close();

            return result;
        }

        public static SQLQueryResult<MachineInformation> GetMachineInformationForEdit(long id)
        {
            return GetRecordForEdit(id, query => GetMachineInformations(query));
        }

        public static SQLQueryResult<MachineInformation> ForceToEditMachineInformation(long id)
        {
            return ForceToEdit<MachineInformation>(id);
        }

        public static void ReleaseEditingMachineInformation(long id)
        {
            ReleaseEditing<MachineInformation>(id);
        }

        public static SQLQueryResult<MachineInformation> MarkForDeleteMachineInformation(MachineInformation machineInformation)
        {
            return MarkForDeletion<MachineInformation>(machineInformation);
        }


        private static SQLQueryResult<T> MarkForDeletion<T>(BaseDatabaseField dbObject, Action postWork = null) where T : BaseDatabaseField
        {
            if (databaseConnection == null)
            {
                CreateConnection();
            }

            var classname = typeof(T).Name.ToUpper();

            dbObject.ModifiedPersonName = Helper.Helper.GetPersonName();
            string query = "UPDATE " + classname + " SET Deleted = " + dbObject.Deleted + ",ModifiedPersonName = '" + dbObject.ModifiedPersonName + "' WHERE ID = " + dbObject.Id;

            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            databaseConnection.Open();
            int records = execQuery.ExecuteNonQuery();
            var result = new SQLQueryResult<T>(records, dbObject.Id);

            Task.Factory.StartNew(() =>
            {
                if (postWork != null)
                {
                    postWork();
                }
            }).ContinueWith((task) =>
            {
                databaseConnection.Close();
            });

            return result;
        }

        public static List<MachineInformation> GetMachineInformations(Location location)
        {
            string query = "SELECT * FROM MACHINEINFORMATION WHERE LOCATIONID = " + location.Id;
            return GetMachineInformations(query);
        }

        public static List<MachineInformation> GetMachineInformationsByLocation(long id)
        {
            string query = "SELECT * FROM MACHINEINFORMATION WHERE LOCATIONID = " + id;
            return GetMachineInformations(query);
        }

        public static List<MachineInformation> GetMachineInformations(long id)
        {
            string query = "SELECT * FROM MACHINEINFORMATION WHERE ID = " + id;
            return GetMachineInformations(query);
        }

        public static List<MachineInformation> GetMachineInformations()
        {
            string query = "SELECT * FROM MACHINEINFORMATION";
            return GetMachineInformations(query);
        }

        private static List<MachineInformation> GetMachineInformations(string query)
        {
            List<MachineInformation> machineinformations = new List<MachineInformation>();

            SelectItems(query, reader =>
            {
                MachineInformation machineinformation = new MachineInformation();
                machineinformation.LocationId = reader.GetInt64("LocationId");
                machineinformation.CustomerId = reader.GetInt64("CustomerId");
                machineinformation.Name = reader.GetString("Name");
                machineinformation.YearOfConstruction = reader.GetDateTime("YearOfConstruction");
                machineinformation.SerialNumber = reader.GetString("SerialNumber");
                machineinformation.HoldingPositions = reader.GetInt32("HoldingPositions");
                machineinformation.Entrances = reader.GetInt32("Entrances");
                machineinformation.Payload = reader.GetInt32("Payload");
                machineinformation.Description = reader.GetString("Description");
                machineinformation.GoogleDriveFolderName = reader.GetString("GoogleDriveFolderName");
                machineinformation.GoogleDriveLink = reader.GetString("GoogleDriveLink");
                machineinformation.Id = reader.GetInt64("Id");
                machineinformation.CreatedDate = reader.GetDateTime("CreatedDate");
                machineinformation.ModifiedDate = reader.GetDateTime("ModifiedDate");
                machineinformation.CreatedPersonName = reader.GetString("CreatedPersonName");
                machineinformation.ModifiedPersonName = reader.GetString("ModifiedPersonName");
                machineinformation.ReadOnly = reader.GetBoolean("ReadOnly");
                machineinformation.UsedBy = reader.GetString("UsedBy");
                machineinformation.Deleted = reader.GetBoolean("Deleted");
                machineinformations.Add(machineinformation);
            });



            foreach (var machineinformation in machineinformations)
            {
                machineinformation.ContactPerson = GetContactPartners(machineinformation.Id,
                    Helper.Helper.ClassTypeForeignKeyTypeMapper[typeof(MachineInformation)]).FirstOrDefault();
            }

            return machineinformations;
        }

        public static List<Customer> GetCustomersOnly()
        {
            string query = "SELECT * FROM CUSTOMER";

            List<Customer> customers = new List<Customer>();

            SelectItems(query, reader =>
            {
                Customer customer = new Customer();
                customer.CompanyName = reader.GetString("CompanyName");
                customer.Address = reader.GetString("Address");
                customer.Postcode = reader.GetString("Postcode");
                customer.City = reader.GetString("City");
                customer.Selected = reader.GetBoolean("Selected");
                customer.AdditionalInfo = reader.GetString("AdditionalInfo");
                customer.GoogleDriveFolderName = reader.GetString("GoogleDriveFolderName");
                customer.GoogleDriveLink = reader.GetString("GoogleDriveLink");
                customer.Id = reader.GetInt64("Id");
                customer.CreatedDate = reader.GetDateTime("CreatedDate");
                customer.ModifiedDate = reader.GetDateTime("ModifiedDate");
                customer.CreatedPersonName = reader.GetString("CreatedPersonName");
                customer.ModifiedPersonName = reader.GetString("ModifiedPersonName");
                customer.ReadOnly = reader.GetBoolean("ReadOnly");
                customer.UsedBy = reader.GetString("UsedBy");
                customers.Add(customer);
            });

            return customers;
        }

        public static SQLQueryResult<MaintenanceAgreement> UpdateMaintenanceAgreement(MaintenanceAgreement maintenanceagreement)
        {
            var dbConnection = GetConnection();

            maintenanceagreement.ModifiedPersonName = Helper.Helper.GetPersonName();
            string query = "UPDATE MAINTENANCEAGREEMENT SET LocationId = " + maintenanceagreement.LocationId + ",CustomerId = " + maintenanceagreement.CustomerId + ",MachineInformationId = " + maintenanceagreement.MachineInformationId + ",Duration = '" + maintenanceagreement.Duration.ToString("yyyy-MM-dd") + "',CanBeCancelled = '" + maintenanceagreement.CanBeCancelled + "',AgreementCancelledBy = '" + maintenanceagreement.AgreementCancelledBy + "',NoticeOfPeriod = " + maintenanceagreement.NoticeOfPeriod + ",AgreementDate = '" + maintenanceagreement.AgreementDate.ToString("yyyy-MM-dd") + "',MaintenanceType = '" + maintenanceagreement.MaintenanceType + "',AdditionalInfo = '" + maintenanceagreement.AdditionalInfo + "',NotificationTime = " + maintenanceagreement.NotificationTime + ",NotificationUnit = " + (int) maintenanceagreement.NotificationUnit + ",GoogleCalendarEventId = '" + maintenanceagreement.GoogleCalendarEventId + "',GoogleDriveFolderName = '" + maintenanceagreement.GoogleDriveFolderName + "',GoogleDriveLink = '" + maintenanceagreement.GoogleDriveLink + "',CreatedPersonName = '" + maintenanceagreement.CreatedPersonName + "',ModifiedPersonName = '" + maintenanceagreement.ModifiedPersonName + "',ReadOnly = " + maintenanceagreement.ReadOnly + ",UsedBy = '" + maintenanceagreement.UsedBy + "',Deleted = " + maintenanceagreement.Deleted + " WHERE ID = "+maintenanceagreement.Id;

            MySqlCommand execQuery = new MySqlCommand(query, dbConnection);

            dbConnection.Open();
            int records = execQuery.ExecuteNonQuery();
            var result = new SQLQueryResult<MaintenanceAgreement>(records, maintenanceagreement.Id);
            dbConnection.Close();

            return result;
        }

        public static SQLQueryResult<MaintenanceAgreement> GetMaintenanceAgreementForEdit(long id)
        {
            return GetRecordForEdit(id, query => GetMaintenanceAgreements(query));
        }

        public static SQLQueryResult<MaintenanceAgreement> ForceToEditMaintenanceAgreement(long id)
        {
            return ForceToEdit<MaintenanceAgreement>(id);
        }

        public static void ReleaseEditingMaintenanceAgreement(long id)
        {
            ReleaseEditing<MaintenanceAgreement>(id);
        }

        public static SQLQueryResult<MaintenanceAgreement> MarkForDeleteMaintenanceAgreement(MaintenanceAgreement maintenanceAgreement)
        {
            return MarkForDeletion<MaintenanceAgreement>(maintenanceAgreement);
        }

        public static List<MaintenanceAgreement> GetMaintenanceAgreementsByMachineInformation(long id)
        {
            string query = "SELECT * FROM MAINTENANCEAGREEMENT WHERE MACHINEINFORMATIONID = " + id;
            return GetMaintenanceAgreements(query);
        }

        public static List<string> GetMaintenanceAgreementTerminationUnits()
        {
            throw new NotImplementedException();
        }

        public static ObservableCollection<ManagementOverviewFilter> GetManagementOverviewFilter()
        {
            ObservableCollection<ManagementOverviewFilter> overview = new ObservableCollection<ManagementOverviewFilter>();

            var customrs = GetCustomers();
            var locations = GetLocations();
            var machine = GetMachineInformations();


            foreach (var machineInformation in machine)
            {
                var item = new ManagementOverviewFilter();

                item.MachineInformationId = machineInformation.Id;
                item.Manufacturer = machineInformation.Name;
                item.SerialNumber = machineInformation.SerialNumber;
                item.YearOfConstruction = machineInformation.YearOfConstruction.ToString("dd.MM.yyyy");
                item.CustomerId = machineInformation.CustomerId;
                item.LocationId = machineInformation.LocationId;


                var location = locations.Where(c => c.Id == machineInformation.LocationId).FirstOrDefault();
                item.Location = string.Format("{0},{1} {2}", location.Address, location.Postcode, location.City);

                var customer = customrs.Where(c => c.Id == machineInformation.CustomerId).FirstOrDefault();
                item.CompanyName = customer.CompanyName;
                item.Administrator = customer.Administrator.Name;

                overview.Add(item);
            }

            return overview;
        }
    }
}
