using System;
using System.Configuration;
using System.Data.SqlClient;
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
            using (SqlConnection connection = new SqlConnection(connectionString))
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
        }
    }
}










//         Console.WriteLine("------------------------------------");
//         Console.WriteLine("Manage Stacks \n");
//         Console.WriteLine("Manage Flashcards \n");
//         Console.WriteLine("Study \n");
//         Console.WriteLine("View Study Session Data \n");
//         Console.WriteLine("------------------------------------");
//     }
// }