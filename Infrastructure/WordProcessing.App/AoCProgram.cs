using WordProcessing.Domain.TokenAnalytics;
using WordProcessing.Domain.TokenReading;
using WordProcessing.IO;

namespace WordProcessing.App;

public class AoCProgram(ITokenAnalyzer part1, ITokenAnalyzer part2) : IProgramCore
{
    public void Run(string[] args)
    {
        using var state = new InputOutputState(args);
        state.OpenInputFile(0);

        var tokenAnalyzer = part1;
        if (args is [_, "-p2"])
        {
            tokenAnalyzer = part2;
        }

        var tokenReader = GetTokenReader(state);

        TokenProcessingApp.Run(tokenReader, tokenAnalyzer);
    }

    protected virtual ITokenReader GetTokenReader(InputOutputState state)
    {
        return new CharReadingLineAwareReader(state.Reader!);
    }
}