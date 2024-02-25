using System.Configuration;

namespace Flashcards;

class InputHelper
{
    static public void GetUserInput()
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine("1: Manage Stacks");
        Console.WriteLine("2: Manage Flashcards");
        Console.WriteLine("3: Study");
        Console.WriteLine("4: View Study Session Data");
        Console.WriteLine("5: Read Data in from Csv Files");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ManageStacks();
                break;
            case "2":
                ManageFlashcards();
                break;
            // case "3":
            //     break;
            // case "4":
            //     break;
            case "5":
                Stack.ReadInFromCsv(ConfigurationManager.AppSettings["stackCsv"]);
                Card.ReadInFromCsv(ConfigurationManager.AppSettings["cardCsv"]);
                break;
            default:
                Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                break;
        }

    }
    static public void ManageStacks()
    {   Console.WriteLine("\n \n");
        Console.WriteLine("1: Add new Stack");
        Console.WriteLine("2: Update Existing Stack");
        Console.WriteLine("3: Delete a Stack");
        Console.WriteLine("0: Back to Main Menu");
        string? choice = Console.ReadLine();

        switch(choice)
        {
            case "1":
                Stack.Add();
                break;
            case "2":
                Stack.Update();
                break;
            case "3":
                Stack.Delete();
                break;
            case "0":
                GetUserInput();
                break;
        }
    }

    static public void ManageFlashcards()
    {
        Console.WriteLine("1: Add new Flashcard");
        Console.WriteLine("2: Update Existing Flashcard");
        Console.WriteLine("3: Delete a Flashcard");
        Console.WriteLine("0: Back to Main Menu");

        string? choice = Console.ReadLine();

        switch(choice)
        {
            case "1":
                Card.Add();
                break;
            case "2":
                Card.Update();
                break;
            case "3":
                Card.Delete();
                break;
            case "0":
                GetUserInput();
                break;
        }
    }
    public static int GetNumberInput(string message){
        // Add format checking here
        Console.WriteLine($"\n{message}\n");

        int input = int.Parse(Console.ReadLine());

        if (input == 0) GetUserInput(); 

        return input;
    }
}