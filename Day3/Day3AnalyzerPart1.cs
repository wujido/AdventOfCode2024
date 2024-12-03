using System.Text.RegularExpressions;
using WordProcessing.Domain.TokenAnalytics;
using WordProcessing.Domain.TokenReading;

namespace Day3;

public partial class Day3AnalyzerPart1 : ITokenAnalyzer
{
    private int Sum { get; set; }

    public void ProcessNextToken(IToken token)
    {
        if (token is not WordToken wt) return;

        var matches = MulInstructionRegex().Matches(wt.Value);

        foreach (Match match in matches)
        {
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);

            Sum += x * y;
        }
    }

    public void Finish()
    {
        Console.WriteLine(Sum);
    }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MulInstructionRegex();
}