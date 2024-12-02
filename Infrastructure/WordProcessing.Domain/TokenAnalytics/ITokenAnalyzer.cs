using WordProcessing.Domain.TokenReading;

namespace WordProcessing.Domain.TokenAnalytics;

public interface ITokenAnalyzer
{
    public void ProcessNextToken(IToken token);
    public void Finish();
}