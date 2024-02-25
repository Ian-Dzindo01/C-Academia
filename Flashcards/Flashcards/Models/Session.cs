using System.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Flashcards;

class StudySession()
{
    DateTime date;
    double score;

    static string connectionString = ConfigurationManager.AppSettings["connectionString"];

    public static void Session()
    {
        Console.WriteLine("Which language do you want to practice?");

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            // Display the names
            string stringQuery = "SELECT name from stack_table";
            string idQuery = "SELECT Id from stack_table";

            var names = connection.Query<string>(stringQuery).ToList();
            var ids = connection.Query<string>(idQuery).ToList();
            var keyValuePairs = names.Zip(ids, (key, value) => new { Key = key, Value = value });

            Dictionary<string, string> languages = keyValuePairs.ToDictionary(pair => pair.Key, pair => pair.Value);

            foreach (string name in names)
                Console.WriteLine(name);
            
            string? choice = Console.ReadLine();
            int intChoice = int.Parse(languages[choice]);

            string idquery = @"SELECT question, answer from card_table WHERE stackId = @intChoice"; 

            var result = connection.Query(idquery, new { intChoice }).ToList();

            foreach (var row in result)
            {
                Console.WriteLine($"Question: {row.question}, Answer: {row.answer}");
            }
        }
    }
}