using WordProcessing.Domain.Puzzles;
using WordProcessing.Domain.TokenReading;

namespace WordProcessing.Domain.TokenAnalytics;

public class CharMatrixAnalyzer(IMatrixPuzzle<char> puzzle) : ITokenAnalyzer
{
    private List<List<char>> Lines { get; set; } = [];
    private List<char> CurrentLine { get; set; } = [];

    public void ProcessNextToken(IToken token)
    {
        switch (token)
        {
            case WordToken word:
                CurrentLine.AddRange(word.Value.Trim().ToCharArray());
                break;
            case EndOfLineToken:
                Lines.Add(CurrentLine.ToList());
                CurrentLine.Clear();
                break;
        }
    }

    public void Finish()
    {
        Lines.Add(CurrentLine.ToList());

        var res = puzzle.Solve(Lines);
        Console.WriteLine(res);
    }
}