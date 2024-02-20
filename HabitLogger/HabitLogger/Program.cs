using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Globalization;

namespace habit_tracker
{
    class Program
    {
        static string connectionString = @"Data Source=habit-Tracker.db";
        static void Main(string[] args)
        {
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS making_money (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Quantity INTEGER
                        )";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            GetUserInput();
        }

        static void GetUserInput()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 to View All Records.");
                Console.WriteLine("Type 2 to Insert Record.");
                Console.WriteLine("Type 3 to Delete Record.");
                Console.WriteLine("Type 4 to Update Record.");
                Console.WriteLine("------------------------------------------\n");

                string command = Console.ReadLine();

                switch (command)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye!\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        GetAllRecords();
                        break;
                    case "2":
                        Insert();
                        break;
                    // case "3":
                    //     Delete();
                    //     break;
                    // case "4":
                    //     Update();
                    //     break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;
                }
            }

        }

        private static void Insert()
        {   
            string date = GetDateInput();

            int quant = GetNumberInput("\n Enter the quantity that you want to input.");

            using(var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = 
                    $"INSERT INTO making_money(date, quantity) VALUES('{date}', {quant})";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        internal static string GetDateInput()
        {
            Console.WriteLine("Please insert the date: (Format: dd-mm-yy). 0 to return to the main menu.");
            // Add format checking here
            string input = Console.ReadLine();

            if (input == "0") GetUserInput();

            return input;
        }

        internal static int GetNumberInput(string message){
            // Add format checking here
            Console.WriteLine($"\n{message}\n");
            int input = int.Parse(Console.ReadLine());

            if (input == 0) GetUserInput(); 

            return input;
        }

        private static void GetAllRecords()
        {
            Console.Clear();
            using(var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"SELECT * FROM making_money";

                List<MakingMoney>  tableData = new();

                SqliteDataReader reader = tableCmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        tableData.Add(new MakingMoney
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", CultureInfo.InvariantCulture),
                            Quantity = reader.GetInt32(2)
                        });
                    }
                }
                else

                {
                    Console.WriteLine("No rows found");
                }

                connection.Close();

                Console.WriteLine("-------------------------------------------\n");
                foreach(var w in tableData)
                {
                    Console.WriteLine($"{w.Id} - {w.Date.ToString("dd-MMM-yyyy")} - Quantity: {w.Quantity}");
                }
                Console.WriteLine("-------------------------------------------\n");
            }
        }

        // private void deleteRecord(int id){

            
        // }


    }

    class MakingMoney
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }

    }
}