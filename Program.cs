
// Get all files in the Puzzles folder that end in .json

using System.Text.Json;
using GeneralPurposeLib;
using PuzzleBox;

string[] files = Directory.GetFiles("Puzzles", "*.json");

Console.WriteLine("Choose a puzzle to play:");

// For each file say the name and index
for (int index = 0; index < files.Length; index++) {
    string file = files[index];
    Console.WriteLine($"{index}. {file}");
}

// Get the user's choice
int choice = int.Parse(Console.ReadLine()!);

// Validate
if (choice < 0 || choice >= files.Length) {
    Console.WriteLine("Invalid choice");
    return;
}

// Load the puzzle
Console.WriteLine("Loading puzzle...");
string json = File.ReadAllText(files[choice]);
Puzzle puzzle = JsonSerializer.Deserialize<Puzzle>(json)!;

int currentQuestion = int.Parse(UserPrefs.GetString($"cq-{puzzle.Name}", "0"));

// On quit, save the current question
Console.CancelKeyPress += (_, _) => {
    UserPrefs.SetString($"cq-{puzzle.Name}", currentQuestion.ToString());
    UserPrefs.Save();
    Console.WriteLine("Goodbye!");
};

Console.Clear();
Console.WriteLine($"==== {puzzle.Name} ====");
Console.WriteLine($"{puzzle.Intro}");
Console.WriteLine();
Console.WriteLine("Press any key to start...");
Console.ReadKey();
Console.WriteLine();

// Play the puzzle
for (; currentQuestion < puzzle.Questions.Length; currentQuestion++) {
    QuestionAnswer question = puzzle.Questions[currentQuestion];
    Console.WriteLine($"{question.Question}");
    string answer = "\n";
    int incorrectAttempts = 0;
    while (!question.Answers.Contains(answer)) {
        foreach (Hint hint in question.Hints) {
            if (hint.FailedAttempts != incorrectAttempts) continue;
            Console.WriteLine("Hint: " + hint.Text);
        }
        Console.Write("> ");
        answer = Console.ReadLine()!;
        
        // Remove symbols
        answer = answer
            .ToLower()
            .Replace(",", "")
            .Replace(".", "")
            .Replace("!", "")
            .Replace("?", "")
            .Replace("'", "")
            .Replace("\"", "");

        Console.WriteLine(question.Answers.Contains(answer)
            ? "Correct!"
            : "Incorrect!");
        incorrectAttempts++;
    }
}
Console.Clear();
Console.WriteLine("================================");
Console.WriteLine(puzzle.WinMsg);
Console.WriteLine("================================");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();