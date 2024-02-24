using System.Configuration;
using Dapper;
using Microsoft.Data.Sqlite;


namespace Flashcards;

class Initializer
{
    static string connectionString = ConfigurationManager.AppSettings["connectionString"];
    public static void Init()
    {
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
                    name TEXT,

                );");

            connection.Close();

        }
    }
}