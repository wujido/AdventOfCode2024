using WordProcessing.Domain;
using WordProcessing.Domain.TokenAnalytics;
using WordProcessing.Domain.TokenReading;
using WordProcessing.Domain.TokenReading.Decorators;
using WordProcessing.IO;

namespace WordProcessing.App;

public class AoCProgram(ITokenAnalyzer part1, ITokenAnalyzer part2) : IProgramCore
{
    public void Run(string[] args)
    {
        using var state = new InputOutputState(args);
        state.OpenInputFile(0);

        var tokenAnalyzer = args[0] == "part2" ? part2 : part1;

        ITokenReader tokenReader = new CharReadingLineAwareReader(state.Reader!);
        tokenReader = new IntTokenReaderDecorator(tokenReader);

        TokenProcessingApp.Run(tokenReader, tokenAnalyzer);
    }
}