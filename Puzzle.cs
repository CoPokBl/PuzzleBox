namespace PuzzleBox; 

public class Puzzle {
    public string Name { get; set; }
    public string Intro { get; set; }
    public string WinMsg { get; set; }
    public QuestionAnswer[] Questions { get; set; }
}