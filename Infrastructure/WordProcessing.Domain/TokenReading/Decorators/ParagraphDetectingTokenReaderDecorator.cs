namespace WordProcessing.Domain.TokenReading.Decorators;

public class ParagraphDetectingTokenReaderDecorator(ITokenReader tokenReader) : ITokenReader
{
    private ITokenReader TokenReader { get; } = tokenReader;

    private IToken? NextToken { get; set; }
    private bool InputStarted { get; set; }

    public IToken ReadNextToken()
    {
        if (NextToken is not null)
            return (NextToken, NextToken = null).Item1;

        var endOfLineCount = 0;
        IToken token;

        while ((token = TokenReader.ReadNextToken()) is EndOfLineToken && InputStarted)
            endOfLineCount++;

        if (!InputStarted && token is WordToken) InputStarted = true;

        if (!InputStarted) return token;

        if (endOfLineCount > 0)
            NextToken = token;

        return endOfLineCount switch
        {
            0 => token,
            1 => new EndOfLineToken(),
            _ => new EndOfParagraphToken()
        };
    }
}