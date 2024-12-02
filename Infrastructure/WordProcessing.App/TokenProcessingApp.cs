using System.IO;
using WordProcessing.Domain;
using WordProcessing.Domain.TokenReading;

namespace WordProcessing.App;

public class TokenProcessingApp
{
    public static void Run(ITokenReader tokenReader, ITokenAnalyzer tokenAnalyzer)
    {
        try
        {
            IToken token;
            while ((token = tokenReader.ReadNextToken()) is not EndOfInputToken)
            {
                tokenAnalyzer.ProcessNextToken(token);
            }

            tokenAnalyzer.Finish();
        }
        catch (IOException)
        {
            throw new FileErrorException();
        }
    }
}