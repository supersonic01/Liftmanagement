
using Liftmanagement.Helper;
using Liftmanagement.Models;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

            string query = "INSERT INTO MACHINEINFORMATION(LocationId,CustomerId,Name,YearOfConstruction,SerialNumber,HoldingPositions,Entrances,Payload,Description,ContactByDefect,AdditionalInfo,CreatedPersonName,ModifiedPersonName,ReadOnly,UsedBy)";
            string values = "VALUE(" + machineinformation.LocationId + "," + machineinformation.CustomerId + ",'" + machineinformation.Name + "','" + machineinformation.YearOfConstruction + "','" + machineinformation.SerialNumber + "'," + machineinformation.HoldingPositions + "," + machineinformation.Entrances + "," + machineinformation.Payload + ",'" + machineinformation.Description + "'," + machineinformation.ContactByDefect + ",'" + machineinformation.AdditionalInfo + "','" + machineinformation.CreatedPersonName + "','" + machineinformation.ModifiedPersonName + "'," + machineinformation.ReadOnly + ",'" + machineinformation.UsedBy + "')";
            query = query + values;


            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            databaseConnection.Open();
            int records = execQuery.ExecuteNonQuery();
            long id = execQuery.LastInsertedId;
            var result = new SQLQueryResult<MachineInformation>(records, id);

            machineinformation.ContactPerson.ForeignKey = (int)id;
            var contactResult = AddContactPartner(machineinformation.ContactPerson);
            //result.AddSQLSubQueryResult(contactResult, result);

            databaseConnection.Close();
            return result;
        }


        public static SQLQueryResult<MaintenanceAgreement> AddMaintenanceAgreement(MaintenanceAgreement maintenanceagreement)
        {
            if (databaseConnection == null)
            {
                CreateConnection();
            }

            string query = "INSERT INTO MAINTENANCEAGREEMENT(LocationId,CustomerId,MachineInformationId,Duration,CanBeCancelled,ArreementCancelledBy,NoticeOfPeriod,AgreementDate,MaintenanceType,AdditionalInfo,CreatedPersonName,ModifiedPersonName,ReadOnly,UsedBy)";
            string values = "VALUE(" + maintenanceagreement.LocationId + "," + maintenanceagreement.CustomerId + "," + maintenanceagreement.MachineInformationId + ",'" + maintenanceagreement.Duration + "','" + maintenanceagreement.CanBeCancelled + "','" + maintenanceagreement.ArreementCancelledBy + "'," + maintenanceagreement.NoticeOfPeriod + ",'" + maintenanceagreement.AgreementDate + "','" + maintenanceagreement.MaintenanceType + "','" + maintenanceagreement.AdditionalInfo + "','" + maintenanceagreement.CreatedPersonName + "','" + maintenanceagreement.ModifiedPersonName + "'," + maintenanceagreement.ReadOnly + ",'" + maintenanceagreement.UsedBy + "')";
            query = query + values;


            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            databaseConnection.Open();
            int records = execQuery.ExecuteNonQuery();
            long id = execQuery.LastInsertedId;

            databaseConnection.Close();

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
        public static SQLQueryResult<Customer> GetCustomerForEdit(long id)
        {
            // TODO check user, if is the same user, editing is possilbe

            string query = "SELECT * FROM CUSTOMER WHERE ID= " + id;
            var customers = GetCustomers(query);

            var result = new SQLQueryResult<Customer>(0, id);
            result.DBRecords = customers;
            var customer = customers.FirstOrDefault();

            if (customer.ReadOnly)
            {

            }
            else
            {
                var updateQuery = "UPDATE CUSTOMER SET READONLY = 1 WHERE ID = " + id;

                if (databaseConnection == null)
                {
                    CreateConnection();
                }

                MySqlCommand execQuery = new MySqlCommand(updateQuery, databaseConnection);

                databaseConnection.Open();
                int records = execQuery.ExecuteNonQuery();

                var subResult = new SQLQueryResult<Customer>(records, id);
                // result.AddSQLSubQueryResult(subResult,result);

                databaseConnection.Close();

                customer = GetCustomers(query).FirstOrDefault();
            }

            return result;
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
            List<Location> locations = new List<Location>();

            string query = "SELECT * FROM LOCATION";

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

        public static List<MaintenanceAgreement> GetMaintenanceAgreements()
        {
            List<MaintenanceAgreement> maintenanceagreements = new List<MaintenanceAgreement>();

            string query = "SELECT * FROM MAINTENANCEAGREEMENT";

            SelectItems(query, reader =>
            {
                MaintenanceAgreement maintenanceagreement = new MaintenanceAgreement();
                maintenanceagreement.LocationId = reader.GetInt64("LocationId");
                maintenanceagreement.CustomerId = reader.GetInt64("CustomerId");
                maintenanceagreement.MachineInformationId = reader.GetInt64("MachineInformationId");
                maintenanceagreement.Duration = reader.GetDateTime("Duration");
                maintenanceagreement.CanBeCancelled = reader.GetString("CanBeCancelled");
                maintenanceagreement.ArreementCancelledBy = reader.GetString("ArreementCancelledBy");
                maintenanceagreement.NoticeOfPeriod = reader.GetInt32("NoticeOfPeriod");
                maintenanceagreement.AgreementDate = reader.GetDateTime("AgreementDate");
                maintenanceagreement.MaintenanceType = reader.GetString("MaintenanceType");
                maintenanceagreement.AdditionalInfo = reader.GetString("AdditionalInfo");
                maintenanceagreement.Id = reader.GetInt64("Id");
                maintenanceagreement.CreatedDate = reader.GetDateTime("CreatedDate");
                maintenanceagreement.ModifiedDate = reader.GetDateTime("ModifiedDate");
                maintenanceagreement.CreatedPersonName = reader.GetString("CreatedPersonName");
                maintenanceagreement.ModifiedPersonName = reader.GetString("ModifiedPersonName");
                maintenanceagreement.ReadOnly = reader.GetBoolean("ReadOnly");
                maintenanceagreement.UsedBy = reader.GetString("UsedBy");
                maintenanceagreements.Add(maintenanceagreement);
            });
            return maintenanceagreements;
        }



        public static List<MachineInformation> GetMachineInformations()
        {
            List<MachineInformation> machineinformations = new List<MachineInformation>();

            string query = "SELECT * FROM MACHINEINFORMATION";

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
                machineinformation.ContactByDefect = reader.GetBoolean("ContactByDefect");
                machineinformation.AdditionalInfo = reader.GetString("AdditionalInfo");
                machineinformation.Id = reader.GetInt64("Id");
                machineinformation.CreatedDate = reader.GetDateTime("CreatedDate");
                machineinformation.ModifiedDate = reader.GetDateTime("ModifiedDate");
                machineinformation.CreatedPersonName = reader.GetString("CreatedPersonName");
                machineinformation.ModifiedPersonName = reader.GetString("ModifiedPersonName");
                machineinformation.ReadOnly = reader.GetBoolean("ReadOnly");
                machineinformation.UsedBy = reader.GetString("UsedBy");
                machineinformations.Add(machineinformation);
            });

            foreach (var machineinformation in machineinformations)
            {
                query = "SELECT * FROM CONTACTPARTNER WHERE FOREIGNKEY = " + machineinformation.Id + " AND FOREIGNKEYTYPE = " + Helper.Helper.ClassTypeForeignKeyTypeMapper[typeof(MachineInformation)];

                SelectItems(query, reader =>
                {
                    ContactPartner contactpartner = new ContactPartner();
                    contactpartner.ForeignKey = reader.GetInt32("ForeignKey");
                    contactpartner.ForeignKeyType = reader.GetInt32("ForeignKeyType");
                    contactpartner.Name = reader.GetString("Name");
                    contactpartner.PhoneWork = reader.GetString("PhoneWork");
                    contactpartner.Mobile = reader.GetString("Mobile");
                    contactpartner.EMail = reader.GetString("EMail");
                    contactpartner.Id = reader.GetInt64("Id");
                    contactpartner.CreatedDate = reader.GetDateTime("CreatedDate");
                    contactpartner.ModifiedDate = reader.GetDateTime("ModifiedDate");
                    contactpartner.CreatedPersonName = reader.GetString("CreatedPersonName");
                    contactpartner.ModifiedPersonName = reader.GetString("ModifiedPersonName");
                    contactpartner.ReadOnly = reader.GetBoolean("ReadOnly");
                    contactpartner.UsedBy = reader.GetString("UsedBy");
                    machineinformation.ContactPerson = contactpartner;
                });
            }
            return machineinformations;
        }


        public static SQLQueryResult<Customer> UpdateCustomer(Customer customer)
        {
            //TOTO do in trasaction
            //Check Timestemp if needed

            if (databaseConnection == null)
            {
                CreateConnection();
            }

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


        private static SQLQueryResult<ContactPartner> UpdateContactPartner(ContactPartner contactpartner)
        {
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
            string query = "UPDATE ADMINISTRATORCOMPANY SET CustomerId = " + administratorcompany.CustomerId + ",Name = '" + administratorcompany.Name + "',CreatedPersonName = '" + administratorcompany.CreatedPersonName + "',ModifiedPersonName = '" + administratorcompany.ModifiedPersonName + "',ReadOnly = " + administratorcompany.ReadOnly + ",UsedBy = '" + administratorcompany.UsedBy + "' WHERE ID = " + administratorcompany.Id;

            MySqlCommand execQuery = new MySqlCommand(query, databaseConnection);

            int records = execQuery.ExecuteNonQuery();

            return new SQLQueryResult<AdministratorCompany>(records, administratorcompany.Id);
        }



        private static void SelectItems(string query, Action<MySqlDataReader> getRecords)
        {
            //TODO check whether its not better to open one time and not for each select....
            if (databaseConnection == null)
            {
                CreateConnection();
            }


            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            MySqlDataReader reader;

            try
            {
                // Open the database
                databaseConnection.Open();

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

            }
            finally
            {

                // Finally close the connection
                databaseConnection.Close();
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
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
    database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            return connectionString;
        }

    }
}
