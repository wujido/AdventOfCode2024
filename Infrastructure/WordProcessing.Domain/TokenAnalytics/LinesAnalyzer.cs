using WordProcessing.Domain.Puzzles;
using WordProcessing.Domain.TokenReading;

namespace WordProcessing.Domain.TokenAnalytics;

public class LinesAnalyzer(ILinesPuzzle puzzle) : ITokenAnalyzer
{
    private List<List<int>> Lines { get; } = [];
    private List<int> CurrentLine { get; } = [];

    public void ProcessNextToken(IToken token)
    {
        switch (token)
        {
            case IntToken it:
                CurrentLine.Add(it.Value);
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