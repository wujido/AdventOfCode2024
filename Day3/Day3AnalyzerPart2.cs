using System.Text.RegularExpressions;
using WordProcessing.Domain.TokenAnalytics;
using WordProcessing.Domain.TokenReading;

namespace Day3;

public partial class Day3AnalyzerPart2 : ITokenAnalyzer
{
    private int Sum { get; set; }

    private bool MulEnabled { get; set; } = true;

    public void ProcessNextToken(IToken token)
    {
        if (token is not WordToken wt) return;

        var matches = MulInstructionRegex().Matches(wt.Value);

        foreach (Match match in matches)
        {
            if (match.Value.StartsWith("mul") && MulEnabled)
            {
                var x = int.Parse(match.Groups[1].Value);
                var y = int.Parse(match.Groups[2].Value);

                Sum += x * y;
            }

            if (match.Value.StartsWith("do")) MulEnabled = true;
            if (match.Value.StartsWith("don't")) MulEnabled = false;
        }
    }

    public void Finish()
    {
        Console.WriteLine(Sum);
    }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don\'t\(\)")]
    private static partial Regex MulInstructionRegex();
}