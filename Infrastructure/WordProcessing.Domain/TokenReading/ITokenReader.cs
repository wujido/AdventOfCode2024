namespace WordProcessing.Domain.TokenReading;

public interface ITokenReader
{
    public IToken ReadNextToken();
}