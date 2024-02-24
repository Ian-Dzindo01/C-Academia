namespace Flashcards;

class Card(string question, string answer, string stackName, int id = 0)
{
    string question = question;
    string answer = answer;
    string stackName = stackName;
    public int id = id;
    
    public static Card FromCsv(string csv)
    {
        string[] data = csv.Split(',');
        Card card = new(data[0], data[1], data[2]);
        return card;
    }
    static void Add()
    {

    }

    static void Delete()
    {

    }

    static void Update()
    {

    }

}