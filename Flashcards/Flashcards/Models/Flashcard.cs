using System.Configuration;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.FileIO;


namespace Flashcards;

class Card(string question, string answer, string stackName)
{
    static string connectionString = ConfigurationManager.AppSettings["connectionString"];
    string question = question;
    string answer = answer;
    string stackId = stackName;

    public static void Add()
    {   
        Console.WriteLine("Original word: ");
        string? question = Console.ReadLine();

        Console.WriteLine("Word translation: ");
        string? answer = Console.ReadLine();

        Console.WriteLine("Id of the stack to which this entry belongs to: ");

        string? stackId = Console.ReadLine();

        using (var connection = new SqliteConnection(connectionString))
        {
            string insertCommand = "INSERT INTO card_table (question, answer, stackId) VALUES (@Question, @Answer, @StackId)";

            connection.Open();

            connection.Execute(insertCommand, new { Question = question, Answer = answer, StackId = stackId });

            connection.Close();
        }

        Console.WriteLine("Successfully inserted new Flashcard. \n");

        InputHelper.GetUserInput();
    }

    public static void Delete()
    {
        int id = InputHelper.GetNumberInput("\nType the ID of the entry you would like to delete: ");

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string deleteQuery = "DELETE FROM card_table WHERE Id = @Id";

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
        {   // Make this
            // Output.GetAllRecords();

            int id = InputHelper.GetNumberInput("\nType the ID of the card you would like to update: ");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM card_table WHERE Id = @Id";

                int rowCount = connection.ExecuteScalar<int>(checkQuery, new { Id = id });

                if (rowCount == 0)
                {
                    Console.WriteLine($"Entry with ID {id} does not exist.\n");
                    connection.Close();
                    Update();
                }

                Console.WriteLine("New question: \n");
                string? question = Console.ReadLine();

                Console.WriteLine("New translation: \n");
                string? answer = Console.ReadLine();

                Console.WriteLine("New Stack Id: \n");
                int stackId = int.Parse(Console.ReadLine());

                string updateQuery = "UPDATE card_table SET Question = @Question, Answer = @Answer, StackId = @StackId WHERE Id = @Id";

                connection.Execute(updateQuery, new { Question = question, Answer = answer, StackId = stackId, Id = id });
                
                Console.WriteLine($"Entry with ID {id} was updated.\n");

                connection.Close();

                }

                InputHelper.GetUserInput();
        }
}