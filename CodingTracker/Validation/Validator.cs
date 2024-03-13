using System.Globalization;
using CodingTracker;

public class Validator
{
    public static DateTime GetTimeInput(string startEnd)
    {
        Console.WriteLine($"Please insert the {startEnd} time: (Format: HH:mm). 0 to return to the main menu.");

        string input = Console.ReadLine();

        DateTime timeInput = DateTime.ParseExact(input, "HH:mm", CultureInfo.InvariantCulture);

        if (input == "0") InputHelper.GetUserInput();

        return timeInput;
    }

    public static int GetNumberInput(string message){
        // Add format checking here
        Console.WriteLine($"\n{message}\n");
        int input = int.Parse(Console.ReadLine());

        if (input == 0) InputHelper.GetUserInput(); 

        return input;
    }
}