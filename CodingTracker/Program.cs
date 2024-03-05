using System.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;
using Spectre.Console;

namespace CodingTracker
{   
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

            InputHelper.GetUserInput();
        }

    }
}