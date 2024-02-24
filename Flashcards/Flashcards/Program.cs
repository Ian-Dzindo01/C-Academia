using System;
using System.Configuration;
using Dapper;

namespace Flashcards
{   
    public class Program
    {
        // Create database
        static void Main(string[] args)
        {
            Initializer.Init();
            
            InputHelper.GetUserInput();
        }
    }
}