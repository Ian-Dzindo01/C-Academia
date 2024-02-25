using System;
using System.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;
using Microsoft.VisualBasic.FileIO;

namespace Flashcards;

class Stack(string name)
{
    string name = name;
    static string connectionString = ConfigurationManager.AppSettings["connectionString"];
    
    public static void ReadInFromCsv(string filePath)
    {
        List<string> names = new List<string>();

        using (TextFieldParser parser = new TextFieldParser(filePath))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            while (!parser.EndOfData)
            {
                // Read current line fields, assuming there is only one field per line
                string[] fields = parser.ReadFields();

                if (fields != null && fields.Length > 0)
                {
                    names.Add(fields[0]);
                }
            }
        }

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            // Display the names
            foreach (string name in names)
            {
                string insertCommand = "INSERT INTO stack_table (name) VALUES (@Name)";
                connection.Execute(insertCommand, new { Name = name });
                connection.Close();
            }
        }

        Console.WriteLine("Stack data read in successfully. Moving back to Main Page. \n");
        // InputHelper.GetUserInput();
    }
    public static void Add()
    {   
        Console.WriteLine("Name of stack you would like to add: ");
        string name = Console.ReadLine();

        using (var connection = new SqliteConnection(connectionString))
        {
            string insertCommand = "INSERT INTO stack_table (name) VALUES (@Name)";

            connection.Open();

            connection.Execute(insertCommand, new { Name = name });

            connection.Close();
        }

        Console.WriteLine("Successfully inserted new Stack. \n");
        InputHelper.GetUserInput();
    }

    public static void Delete()
    {
        int id = InputHelper.GetNumberInput("\nType the ID of the entry you would like to delete: ");

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string deleteQuery = "DELETE FROM stack_table WHERE Id = @Id";

            int rowCount = connection.Execute(deleteQuery, new { Id = id });

            if (rowCount == 0){
                Console.WriteLine($"Row {id} does not exist.\n");
                Delete();}
            else
                Console.WriteLine($"Stack {id} was deleted.");

            connection.Close();
        }

        InputHelper.GetUserInput();
    }

    public static void Update()
        {   // Make this
            // Output.GetAllRecords();

            int id = InputHelper.GetNumberInput("\nType the ID of the field you would like to update: ");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM stack_table WHERE Id = @Id";

                int rowCount = connection.ExecuteScalar<int>(checkQuery, new { Id = id });

                if (rowCount == 0)
                {
                    Console.WriteLine($"Entry with ID {id} does not exist.\n");
                    connection.Close();
                    Update();
                }

                Console.WriteLine("Type the new name of this stack: \n");
                string name = Console.ReadLine();

                string updateQuery = "UPDATE stack_table SET Name = @Name WHERE Id = @Id";
                connection.Execute(updateQuery, new { Name = name, Id = id });
                
                Console.WriteLine($"Entry with ID {id} was updated.\n");

                connection.Close();
                }
                InputHelper.GetUserInput();
        }
}