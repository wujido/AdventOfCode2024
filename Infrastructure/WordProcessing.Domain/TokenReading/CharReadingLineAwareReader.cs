using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordProcessing.Domain.TokenReading;

public class CharReadingLineAwareReader(TextReader reader) : ITokenReader
{
    private TextReader Reader { get; } = reader;
    private IToken? NextToken { get; set; }

    public IToken ReadNextToken()
    {
        if (NextToken != null)
        {
            var t = NextToken;
            NextToken = null;
            return t;
        }

        var word = new StringBuilder();

        int read;
        while ((read = Reader.Read()) != -1)
        {
            var ch = (char)read;

            if (ch == '\r') continue;

            switch (ch)
            {
                case '\n':
                    if (word.Length == 0) return new EndOfLineToken();

                    NextToken = new EndOfLineToken();
                    return FinishWord(word);

                case ' ' or '\t':
                {
                    if (word.Length > 0) return FinishWord(word);

                    break;
                }
                default:
                    word.Append(ch);
                    break;
            }
        }

        if (word.Length > 0) return new WordToken(word.ToString());

        return new EndOfInputToken();
    }

    private static IToken FinishWord(StringBuilder word)
    {
        var w = word.ToString();
        word.Clear();
        return new WordToken(w);
    }
}