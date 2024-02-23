namespace Flashcards;

class Stack(string name, int id = 0)
{
    string name = name;
    int id = id;
    
    public static Stack FromCsv(string csv)
    {
        string[] data = csv.Split(',');
        Stack stack = new(data[0]);
        return stack;
    }
}