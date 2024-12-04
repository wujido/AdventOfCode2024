using WordProcessing.Domain.Puzzles;
using WordProcessing.Domain.TokenReading;

namespace WordProcessing.Domain.TokenAnalytics;

public class CharMatrixAnalyzer(IMatrixPuzzle<char> puzzle) : ITokenAnalyzer
{
    public void ProcessNextToken(IToken token)
    {
        throw new NotImplementedException();
    }

    public void Finish()
    {
        throw new NotImplementedException();
    }
}