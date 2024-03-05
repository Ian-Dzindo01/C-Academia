namespace Flashcards;

public class CardDTO
{
    public string Question { get; set; }
    public string Answer { get; set; }

    public CardDTO() { }
    
    public CardDTO(string question, string answer) : this()
    {
        Question = question;
        Answer = answer;
    }
}