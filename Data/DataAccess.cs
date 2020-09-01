using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Models;
using Microsoft.Data.Sqlite;

namespace Liftmanagement.Data
{
    public static class DataAccess
    {
        private static SQLiteConnection connection = null;

        public static void CreateDB()
        {
            string DBName = "Liftmanagement.sqlite";
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DBName);
            Console.WriteLine(dbPath);

            SQLiteConnection.CreateFile(dbPath);
            //con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            connection = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");
        }
        private static void GetDBInfo()
        {
            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";

            var con = new SQLiteConnection(cs);
            con.Open();

            var cmd = new SQLiteCommand(stm, con);
            string version = cmd.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version: {version}");

            con.Close();
        }
        public static void CreateTables()
        {
           var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "DROP TABLE IF EXISTS CUSTOMERS";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE CUSTOMERS (CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
                                                        COMPANYNAME TEXT, CONTACTPERSON TEXT, ADDRESS TEXT, 
                                                        POSTCODE TEXT, CITY TEXT, PHONEWORK TEXT, MOBILE TEXT, 
                                                        SELECTED INTEGER, CREATEDDATE TEXT, CHANGEDDATE TEXT, 
                                                        CHANGEDBY TEXT )";
            cmd.ExecuteNonQuery();
        }
        public static void AddCustomer(Customer customer)
        {
            var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        public static void Test()
        {


        }
    }
}
