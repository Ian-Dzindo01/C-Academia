using System;
using System.Globalization;


namespace CodingTracker
{
    public static class InputHelper
    {                
        public static void GetUserInput()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 to View All Records.");
                Console.WriteLine("Type 2 to Insert Record.");
                Console.WriteLine("Type 3 to Delete Record.");
                Console.WriteLine("Type 4 to Update Record.");
                Console.WriteLine("------------------------------------------\n");

                string command = Console.ReadLine();

                switch (command)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye!\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        Output.GetAllRecords();
                        break;
                    case "2":
                        Controller.Insert();
                        break;
                    case "3":
                        Controller.Delete();
                        break;
                    case "4":
                        Controller.Update();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;
                }
            }

        }

        public static DateTime GetTimeInput(string startEnd)
        {
            Console.WriteLine($"Please insert the {startEnd} time: (Format: HH:mm). 0 to return to the main menu.");

            string input = Console.ReadLine();

            DateTime timeInput = DateTime.ParseExact(input, "HH:mm", CultureInfo.InvariantCulture);

            if (input == "0") GetUserInput();

            return timeInput;
        }

        public static int GetNumberInput(string message){
            // Add format checking here
            Console.WriteLine($"\n{message}\n");
            int input = int.Parse(Console.ReadLine());

            if (input == 0) GetUserInput(); 

            return input;
        }
    }
}