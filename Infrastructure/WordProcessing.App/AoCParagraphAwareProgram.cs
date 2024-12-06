using WordProcessing.Domain;
using WordProcessing.Domain.TokenAnalytics;
using WordProcessing.Domain.TokenReading;
using WordProcessing.Domain.TokenReading.Decorators;
using WordProcessing.IO;

namespace WordProcessing.App;

public class AoCParagraphAwareProgram(ITokenAnalyzer part1, ITokenAnalyzer part2) : AoCProgram(part1, part2)
{
    protected override ITokenReader GetTokenReader(InputOutputState state)
    {
        var tokenReader = base.GetTokenReader(state);
        return new ParagraphDetectingTokenReaderDecorator(tokenReader);
    }
}