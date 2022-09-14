namespace PuzzleBox; 

public class QuestionAnswer {
    public string Question { get; set; }
    public string[] Answers { get; set; }
    public Hint[] Hints { get; set; }
}

public class Hint {
    public int FailedAttempts { get; set; }
    public string Text { get; set; }
}