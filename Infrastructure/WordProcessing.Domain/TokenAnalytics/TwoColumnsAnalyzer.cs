using WordProcessing.Domain.Puzzles;
using WordProcessing.Domain.TokenReading;

namespace WordProcessing.Domain.TokenAnalytics;

public class TwoColumnsAnalyzer(ITwoListPuzzle twoListPuzzle) : ITokenAnalyzer
{
    private List<int> LeftList { get; set; } = [];
    private List<int> RightList { get; set; } = [];

    private bool LastAddedToLeft { get; set; }

    public void ProcessNextToken(IToken token)
    {
        if (token is not IntToken it) return;

        if (LastAddedToLeft)
        {
            RightList.Add(it.Value);
        }
        else
        {
            LeftList.Add(it.Value);
        }

        LastAddedToLeft = !LastAddedToLeft;
    }

    public void Finish()
    {
        var res = twoListPuzzle.Solve(LeftList, RightList);
        Console.WriteLine(res);
    }
}