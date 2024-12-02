using WordProcessing.Domain.TokenReading;

namespace WordProcessing.Domain;

public interface ITokenAnalyzer
{
    public void ProcessNextToken(IToken token);
    public void Finish();
}