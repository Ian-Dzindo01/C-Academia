using System.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;
using Spectre.Console;

namespace CodingTracker{
    class Output 
    {
        static string connectionString = ConfigurationManager.AppSettings["connectionString"];
        public static void GetAllRecords()
        {
            Console.Clear();
            using(var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                List<CodingSession> tableData = connection.Query<CodingSession>("SELECT * FROM coding_tracker").ToList();

                connection.Close();

                Console.WriteLine("-------------------------------------------\n");
                foreach (var w in tableData)
                {
                    TimeSpan duration = w.EndTime - w.StartTime;
                    
                    AnsiConsole.MarkupLine($"[bold]Id:[/] {w.Id} [bold]StartTime:[/] {w.StartTime} [bold]EndTime:[/] {w.EndTime} [bold]Duration:[/] {duration:hh\\:mm}");
                }
                Console.WriteLine("-------------------------------------------\n");
            }
        }
    }
}