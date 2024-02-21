using System;
using System.Configuration;
using System.Globalization;
// using CodingTracker;
using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;

namespace CodingTracker
{
    public class CodingSession
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan duration;

        // Constructor for initializing a CodingSession
        public CodingSession(int id, DateTime startTime, DateTime endTime)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            duration = EndTime - StartTime;
        }
    }
    public class Program
    {
        static string connectionString = ConfigurationManager.AppSettings["connectionString"];
        // Create database
        static void Main(string[] args)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                connection.Execute(@"
                    CREATE TABLE IF NOT EXISTS coding_tracker (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime TEXT,
                        EndTime TEXT,
                        Duration TEXT
                    )");    

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
                        // GetAllRecords();
                        break;
                    case "2":
                        Insert();
                        break;
                    case "3":
                        Delete();
                        break;
                    case "4":
                        Update();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;
                }
            }

        }

        internal static DateTime GetTimeInput(string startEnd)
        {
            Console.WriteLine($"Please insert the {startEnd} time: (Format: HH:mm). 0 to return to the main menu.");

            string input = Console.ReadLine();

            DateTime timeInput = DateTime.ParseExact(input, "HH:mm", CultureInfo.InvariantCulture);

            if (input == "0") GetUserInput();

            return timeInput;
        }

        internal static int GetNumberInput(string message){
            // Add format checking here
            Console.WriteLine($"\n{message}\n");
            int input = int.Parse(Console.ReadLine());

            if (input == 0) GetUserInput(); 

            return input;
        }

        private static void Insert()
        {   
            DateTime startTime = GetTimeInput("start");
            DateTime endTime = GetTimeInput("end");
            TimeSpan duration = endTime - startTime;

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                connection.Execute(
                    "INSERT INTO coding_tracker (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)",
                    new { StartTime = startTime, EndTime = endTime, Duration = duration });

                connection.Close();
            }
        }

        private static void Delete()
        {
            int id = GetNumberInput("\nType the ID of the entry you would like to delete: ");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM coding_tracker WHERE Id = @Id";
                int rowCount = connection.Execute(deleteQuery, new { Id = id });

                if (rowCount == 0){
                    Console.WriteLine($"Row {id} does not exist.\n");
                    Delete();}
                else
                    Console.WriteLine($"Record {id} was deleted.");

                connection.Close();
            }

            GetUserInput();
        }

        private static void Update()
        {
            // GetAllRecords();

            int id = GetNumberInput("\nType the ID of the field you would like to update: ");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM coding_tracker WHERE Id = @Id";
                int rowCount = connection.ExecuteScalar<int>(checkQuery, new { Id = id });

                if (rowCount == 0)
                {
                    Console.WriteLine($"Entry with ID {id} does not exist.\n");
                    connection.Close();
                    Update();
                }

                DateTime startTime = GetTimeInput("start");
                DateTime endTime = GetTimeInput("end");
                TimeSpan duration = endTime - startTime;

                string updateQuery = "UPDATE coding_tracker SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration WHERE Id = @Id";
                connection.Execute(updateQuery, new { StartTime = startTime, EndTime = endTime, Duration = duration, Id = id });
                
                // connection.Execute(
                //     "INSERT INTO coding_tracker (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)",
                //     new { StartTime = startTime, EndTime = endTime, Duration = duration });

                Console.WriteLine($"Entry with ID: {id} was updated.\n");

                connection.Close();
                }

                GetUserInput();

                }
            }
        }





// DateTime now = DateTime.Now;
// Console.WriteLine("Write your time");
// string start = Console.ReadLine();
// string end = Console.ReadLine();


// DateTime startTime = DateTime.ParseExact(start, "HH:mm", CultureInfo.InvariantCulture);
// DateTime endTime = DateTime.ParseExact(end, "HH:mm", CultureInfo.InvariantCulture);

// Console.WriteLine(endTime - startTime);
