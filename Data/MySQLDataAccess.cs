using Liftmanagement.Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Liftmanagement.Data
{
    public static class MySQLDataAccess
    {
       public static void TestConnection()
        {
            //https://dbadmin.hosteurope.de/phpmyadmin/sql.php?server=1&db=db1096358-liftmanagement&table=TestDBRex&pos=0
            //DROP TABLE `AdministratorCompany`, `Customer`;

            string connectionString = GetConnectionString();

            // var connection = new MySqlConnection(connectionString);




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
