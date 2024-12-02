using System.Net.Mime;
using WordProcessing.Domain.TokenReading;

namespace WordProcessing.Domain.Tests;

public class LineAwareTokenReaderTests
{
    public static IEnumerable<object[]> TokenReaders =>
    [
        [(TextReader reader) => new CharReadingLineAwareReader(reader)]
    ];


    [Theory]
    [MemberData(nameof(TokenReaders))]
    public void EmptyInput_OnlyEndOfInput(Func<TextReader, ITokenReader> tokenReaderFactory)
    {
        const string input = "";
        IEnumerable<IToken> expectedTokens = [new EndOfInputToken()];

        RunTokenReaderTest(tokenReaderFactory, input, expectedTokens);
    }

    [Theory]
    [MemberData(nameof(TokenReaders))]
    public void NewLineCharacter_DetectedAsEndOfLineToken(Func<TextReader, ITokenReader> tokenReaderFactory)
    {
        const string input = "\n";
        IEnumerable<IToken> expectedTokens =
        [
            new EndOfLineToken(),
            new EndOfInputToken()
        ];

        RunTokenReaderTest(tokenReaderFactory, input, expectedTokens);
    }

    [Theory]
    [MemberData(nameof(TokenReaders))]
    public void SingleWord_WordTokenThenEndOfInput(Func<TextReader, ITokenReader> tokenReaderFactory)
    {
        const string input = "test";
        IEnumerable<IToken> expectedTokens =
        [
            new WordToken("test"),
            new EndOfInputToken()
        ];

        RunTokenReaderTest(tokenReaderFactory, input, expectedTokens);
    }

    [Theory]
    [MemberData(nameof(TokenReaders))]
    public void MultipleWords_SingleSpace_CorrectTokens(Func<TextReader, ITokenReader> tokenReaderFactory)
    {
        const string input = "test test2";
        IEnumerable<IToken> expectedTokens =
        [
            new WordToken("test"),
            new WordToken("test2"),
            new EndOfInputToken()
        ];

        RunTokenReaderTest(tokenReaderFactory, input, expectedTokens);
    }

    [Theory]
    [MemberData(nameof(TokenReaders))]
    public void MultipleWords_SingleTab_CorrectTokens(Func<TextReader, ITokenReader> tokenReaderFactory)
    {
        const string input = "test\ttest2";
        IEnumerable<IToken> expectedTokens =
        [
            new WordToken("test"),
            new WordToken("test2"),
            new EndOfInputToken()
        ];

        RunTokenReaderTest(tokenReaderFactory, input, expectedTokens);
    }

    [Theory]
    [MemberData(nameof(TokenReaders))]
    public void MultipleWords_MultipleWhiteChars_CorrectTokens(Func<TextReader, ITokenReader> tokenReaderFactory)
    {
        const string input = "test \t  \t test2";
        IEnumerable<IToken> expectedTokens =
        [
            new WordToken("test"),
            new WordToken("test2"),
            new EndOfInputToken()
        ];

        RunTokenReaderTest(tokenReaderFactory, input, expectedTokens);
    }

    [Theory]
    [MemberData(nameof(TokenReaders))]
    public void MultipleWords_NewLineCharBetween_CorrectTokens(Func<TextReader, ITokenReader> tokenReaderFactory)
    {
        const string input = "test\ntest2";
        IEnumerable<IToken> expectedTokens =
        [
            new WordToken("test"),
            new EndOfLineToken(),
            new WordToken("test2"),
            new EndOfInputToken()
        ];

        RunTokenReaderTest(tokenReaderFactory, input, expectedTokens);
    }

    [Theory]
    [MemberData(nameof(TokenReaders))]
    public void MultipleWords_MixedWhiteCharsAndNewLines_CorrectTokens(
        Func<TextReader, ITokenReader> tokenReaderFactory)
    {
        const string input = "\ttest  \n  test2 \t\t";
        IEnumerable<IToken> expectedTokens =
        [
            new WordToken("test"),
            new EndOfLineToken(),
            new WordToken("test2"),
            new EndOfInputToken()
        ];

        RunTokenReaderTest(tokenReaderFactory, input, expectedTokens);
    }

    [Theory]
    [MemberData(nameof(TokenReaders))]
    public void CarriageReturn_IsIgnored(Func<TextReader, ITokenReader> tokenReaderFactory)
    {
        const string input = "w1\r w\r2";
        IEnumerable<IToken> expectedTokens =
        [
            new WordToken("w1"),
            new WordToken("w2"),
            new EndOfInputToken()
        ];

        RunTokenReaderTest(tokenReaderFactory, input, expectedTokens);
    }

    private static void RunTokenReaderTest(Func<TextReader, ITokenReader> tokenReaderFactory, string input,
        IEnumerable<IToken> expectedTokens)
    {
        var tokenReader = tokenReaderFactory(new StringReader(input));

        foreach (var token in expectedTokens)
        {
            Assert.Equal(token, tokenReader.ReadNextToken());
        }
    }
}