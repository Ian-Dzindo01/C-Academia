using System;

namespace Flashcards;

class InputHelper
{
    static public void GetUserInput()
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Manage Stacks \n");
        Console.WriteLine("Manage Flashcards \n");
        Console.WriteLine("Study \n");
        Console.WriteLine("View Study Session Data \n");
        string choice = Console.ReadLine();

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
            default:
                Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                break;
                
        }

    }

    static public void ManageStacks()
    {
        Console.WriteLine("1: Add new Stack \n");
        Console.WriteLine("2: Update Existing Stack \n");
        Console.WriteLine("3: Delete a Stack \n");
        Console.WriteLine("0: Back to Main Menu \n");
        string choice = Console.ReadLine();

        switch(choice)
        {
            case "1":
                break;
            case "2":
                break;
            case "3":
                break;
            case "0":
                GetUserInput();
                break;
        }
    }

    static public void ManageFlashcards()
    {
        Console.WriteLine("1: Add new Flashcard \n");
        Console.WriteLine("2: Update Existing Flashcard \n");
        Console.WriteLine("3: Delete a Flashcard \n");
        Console.WriteLine("0: Back to Main Menu \n");
        
        string choice = Console.ReadLine();

        switch(choice)
        {
            case "1":
                break;
            case "2":
                break;
            case "3":
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