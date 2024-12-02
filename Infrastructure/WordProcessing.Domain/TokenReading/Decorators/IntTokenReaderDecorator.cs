namespace WordProcessing.Domain.TokenReading.Decorators;

public class IntTokenReaderDecorator(ITokenReader reader) : ITokenReader
{
    private ITokenReader Reader { get; } = reader;

    public IToken ReadNextToken()
    {
        var token = Reader.ReadNextToken();

        if (token is WordToken wt && int.TryParse(wt.Value, out var value))
        {
            return new IntToken(value);
        }

        return token;
    }
}