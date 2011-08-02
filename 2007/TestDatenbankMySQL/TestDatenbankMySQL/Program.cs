using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace TestDatenbankMySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Dieses Demoprojekt soll nur die simplen Datenbank-F�lle aufzeigen.
             * Das Kapitel ADO.NET ist sehr weitl�ufig, sodass hier erstmal nur das "einfachste"  gezeigt wird.
             * LINQ to SQL und andere O/R Mapper sind zudem ebenfalls ein interessantes Mittel, was ich sp�ter
             * beleuchten werde.
             * Der Code funktioniert auch nur, wenn die IDs fortlaufend sind.
             */
            MySqlConnection connection = new MySqlConnection(@"Server=127.0.0.1;Uid=root;Pwd=;Database=dotnet;");
            Console.WriteLine("[ Datenbankverbindung �ffnen ]");
            connection.Open();


            // Create
            Console.WriteLine("[ In DB schreiben ]");
            MySqlCommand insertCommand = new MySqlCommand("INSERT INTO test (value) VALUES ('Test')", connection);
            int i = insertCommand.ExecuteNonQuery();
            Console.WriteLine("[ R�ckgabewerte von ExecuteNonQuery: " + i.ToString() + " ]");

            // Read
            Console.WriteLine("[ Aus DB lesen ]");
            MySqlCommand readCommand = new MySqlCommand("SELECT * FROM test", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(readCommand);
            DataTable datatable = new DataTable();

            adapter.Fill(datatable);
            for (int x = 0; x < datatable.Rows.Count; x++)
            {
                object[] values = datatable.Rows[x].ItemArray;
                Console.WriteLine("[ Values: " + values[0].ToString() + " - " + values[1].ToString() + " ]");
            }

            // Update
            Console.WriteLine("[ DB Werte (alle) ver�ndern ]");
            MySqlCommand updateCommand = new MySqlCommand("UPDATE test SET value = 'UpdatedTest'", connection);
            int updatedReturnValue = updateCommand.ExecuteNonQuery();
            Console.WriteLine("[ R�ckgabewerte von ExecuteNonQuery: " + updatedReturnValue.ToString() + " ]");

            //Delete ( sobald mehr als 10 Zeilen in der DB drin sind, werden alle gel�scht
            if (datatable.Rows.Count > 10)
            {
                Console.WriteLine("[ DB Zeile l�schen ]");
                MySqlCommand deleteCommand = new MySqlCommand("DELETE FROM test", connection);
                int deleteReturnValue = deleteCommand.ExecuteNonQuery();
                Console.WriteLine("[ R�ckgabewerte von ExecuteNonQuery: " + deleteReturnValue.ToString() + " ]");
            }
            

            Console.WriteLine("[ Datenbankverbindung schlie�en ]");
            connection.Close();
            Console.Read();
        }
    }
}
