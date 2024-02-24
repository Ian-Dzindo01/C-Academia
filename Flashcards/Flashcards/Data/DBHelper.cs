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
                stackId INTEGER,  
                FOREIGN KEY (stackId) REFERENCES stack_table(Id) ON DELETE CASCADE
            );");

            connection.Close();
            
        }
    }
}