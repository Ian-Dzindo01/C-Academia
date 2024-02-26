using System.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Flashcards;

class Session()
{ 
    public int correct = 0;
    public int wrong = 0;
}

class Games()
{
    DateTime date;
    double score;

    static string connectionString = ConfigurationManager.AppSettings["connectionString"];


    // Separate this function
    public static void Game()
    {
        Console.WriteLine("Which language do you want to practice?");

        Random rnd = new Random();

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
            string response;
            Session session = new Session();

            for(int i = 1; i <= result.Count; i++)
            {
                var obj = result[rnd.Next(1, result.Count)];

                Console.WriteLine($"{obj.question} translates to: ");

                response = Console.ReadLine();

                if (response != obj.answer)
                {   
                    session.wrong ++;
                    Console.WriteLine("Wrong answer");
                }
                else
                {   
                    session.correct++;
                    Console.WriteLine("Correct");
                }
            }
            Console.WriteLine($"You had {session.correct} correct answers and {session.wrong} wrong answers.");
            Console.WriteLine("This session will be store in the database.");
        }
    }
}