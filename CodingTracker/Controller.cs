using System.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;
using Spectre.Console;

namespace CodingTracker 
{
    class Controller 
    {
        static string connectionString = ConfigurationManager.AppSettings["connectionString"];
        public static void Insert()
        {   
            DateTime startTime = InputHelper.GetTimeInput("start");
            DateTime endTime = InputHelper.GetTimeInput("end");
            TimeSpan duration = endTime - startTime;
            string formattedDuration = duration.ToString(@"hh\:mm");


            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                connection.Execute(
                    "INSERT INTO coding_tracker (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)",
                    new { StartTime = startTime, EndTime = endTime, Duration = formattedDuration });

                connection.Close();
            }
        }

        public static void Delete()
        {
            int id = InputHelper.GetNumberInput("\nType the ID of the entry you would like to delete: ");

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

            InputHelper.GetUserInput();
        }

        public static void Update()
        {
            Output.GetAllRecords();

            int id = InputHelper.GetNumberInput("\nType the ID of the field you would like to update: ");

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

                DateTime startTime = InputHelper.GetTimeInput("start");
                DateTime endTime = InputHelper.GetTimeInput("end");
                TimeSpan duration = endTime - startTime;
                string formattedDuration = duration.ToString(@"hh\:mm");


                string updateQuery = "UPDATE coding_tracker SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration WHERE Id = @Id";
                connection.Execute(updateQuery, new { StartTime = startTime, EndTime = endTime, Duration = formattedDuration, Id = id });
                
                Console.WriteLine($"Entry with ID: {id} was updated.\n");

                connection.Close();
                }

                InputHelper.GetUserInput();
                }
       }
}