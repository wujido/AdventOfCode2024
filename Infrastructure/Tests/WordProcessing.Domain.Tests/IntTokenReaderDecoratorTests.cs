using WordProcessing.Domain.TokenReading;
using WordProcessing.Domain.TokenReading.Decorators;

namespace WordProcessing.Domain.Tests;

public class IntTokenReaderDecoratorTests
{
    [Fact]
    public void EndOfInputToken_IsRepeated()
    {
        IEnumerable<IToken> inputTokens =
        [
            new EndOfInputToken(),
        ];
        IEnumerable<IToken> expectedTokens =
        [
            new EndOfInputToken(),
        ];

        RunDecoratorTest(inputTokens, expectedTokens);
    }


    [Fact]
    public void WordToken_WithNonNumericToken_IsRepeated()
    {
        IEnumerable<IToken> inputTokens =
        [
            new WordToken("test"),
        ];
        IEnumerable<IToken> expectedTokens =
        [
            new WordToken("test"),
        ];

        RunDecoratorTest(inputTokens, expectedTokens);
    }

    [Fact]
    public void WordToken_WithValidInt_IsConvertedToIntToken()
    {
         IEnumerable<IToken> inputTokens =
         [
             new WordToken("1"),
         ];
         IEnumerable<IToken> expectedTokens =
         [
             new IntToken(1),
         ];

         RunDecoratorTest(inputTokens, expectedTokens);
    }

    [Fact]
    public void WordToken_WithRealNumber_IsRepeated()
    {
         IEnumerable<IToken> inputTokens =
         [
             new WordToken("1.1"),
         ];
         IEnumerable<IToken> expectedTokens =
         [
             new WordToken("1.1"),
         ];

         RunDecoratorTest(inputTokens, expectedTokens);
    }

    [Fact]
    public void EndOfLineToken_IsRepeated()
    {
          IEnumerable<IToken> inputTokens =
          [
              new EndOfLineToken(),
          ];
          IEnumerable<IToken> expectedTokens =
          [
              new EndOfLineToken(),
          ];

          RunDecoratorTest(inputTokens, expectedTokens);
    }

    private static void RunDecoratorTest(IEnumerable<IToken> inputTokens, IEnumerable<IToken> expectedTokens)
    {
        var tokenReader = new IntTokenReaderDecorator(new WordRepeatingWordReader(inputTokens));

        foreach (var token in expectedTokens)
        {
            Assert.Equal(token, tokenReader.ReadNextToken());
        }
    }

    private class WordRepeatingWordReader(IEnumerable<IToken> inputTokens) : ITokenReader
    {
        private Queue<IToken> Tokens { get; set; } = new(inputTokens);

        public IToken ReadNextToken() =>
            Tokens.TryDequeue(out var token) ? token : new EndOfInputToken();
    }
}