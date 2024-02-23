using System;
using System.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Flashcards
{   
    public class Program
    {
        static string connectionString = ConfigurationManager.AppSettings["connectionString"];
        // Create database
        static void Main(string[] args)
        {
        // Create a connection
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                connection.Execute(@"
                    CREATE TABLE IF NOT EXISTS stack_table (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        name TEXT
                    );

                    CREATE TABLE IF NOT EXISTS card_table (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        question TEXT,
                        answer TEXT,
                        name TEXT
                    );");

                connection.Close();
            }
            
            InputHelper.GetUserInput();
        }
    }
}