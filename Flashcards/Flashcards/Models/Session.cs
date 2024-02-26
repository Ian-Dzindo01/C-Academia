using System.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;
using Spectre.Console;

namespace Flashcards;

class Session()
{   
    public DateTime time;
    public int correct = 0;
    public int wrong = 0;
}

class Games()
{
    DateTime date;
    double score;

    static string connectionString = ConfigurationManager.AppSettings["connectionString"];
    // Separate this function
    public static void GamePrep()
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
            int stackChoice = int.Parse(languages[choice]);

            string idquery = @"SELECT question, answer from card_table WHERE stackId = @stackChoice"; 

            var result = connection.Query(idquery, new { stackChoice }).ToList();
            Game(result, stackChoice);
            connection.Close();
        }

        InputHelper.GetUserInput();
}

    private static void Game(List<dynamic> result, int stackId)
    {
        string response;
        Session session = new Session();
        Random rnd = new Random();

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

            session.time = DateTime.Now;
            Console.WriteLine(DateTime.Now);

            Console.WriteLine($"You had {session.correct} correct answers and {session.wrong} wrong answers.");
            Console.WriteLine("This session will be stored in the database.");



            // 2 connections open at once. Might be a problem.
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string insertCommand = @"INSERT INTO session_table (time, correct, wrong, stackId) VALUES (@Time, @Correct, @Wrong, @StackId)";

                connection.Execute(insertCommand, new { Time = session.time, Correct = session.correct, Wrong = session.wrong, StackId = stackId });
                connection.Close();
            }
    }

    public static void ShowSessionTables()
    {
        using(var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            List<Session> tableData = connection.Query<Session>("SELECT * FROM session_table").ToList();

            connection.Close();

            Console.WriteLine("-------------------------------------------\n");
            foreach (var w in tableData)
            {                
                AnsiConsole.MarkupLine($"[bold]Date and Time:[/] {w.time} [bold]Correct Answers:[/] {w.correct} [bold]Wrong Answers:[/] {w.wrong}");
            }
            Console.WriteLine("-------------------------------------------\n");
        }

        InputHelper.GetUserInput();
    }
}