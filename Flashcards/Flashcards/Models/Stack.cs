using System;
using System.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Flashcards;

class Stack(string name)
{
    string name = name;

    static string connectionString = ConfigurationManager.AppSettings["connectionString"];
    
    public static Stack FromCsv(string csv)
    {
        string[] data = csv.Split(',');
        Stack stack = new(data[0]);
        return stack;
    }
    static void Add(string name)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            string insertCommand = "INSERT INTO stack_table (name) VALUES (@Name)";

            connection.Open();

            connection.Execute(insertCommand, new { Name = name });

            connection.Close();

            Console.WriteLine("Successfully inserted new Stack. \n");

    }

    static void Delete()
    {
        int id = InputHelper.GetNumberInput("\nType the ID of the entry you would like to delete: \n");

        using(var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = 
                $"DELETE FROM making_money where Id = '{id}'";

            int rowCount = tableCmd.ExecuteNonQuery();

            if (rowCount == 0){
                Console.WriteLine($"Row {id} does not exist.\n");
                Delete();
            }


    }

    static void Update()
    {

    }
    }
    }
}