using System.Numerics;
using WordProcessing.Domain.TokenAnalytics;
using WordProcessing.Domain.TokenReading;

internal class Day7Analyzer(IDay7Puzzle puzzle) : ITokenAnalyzer
{
    private List<CalibrationEquation> Equations { get; } = [];
    private BigInteger CurrentResult { get; set; } = 0;
    private List<BigInteger> CurrentNumbers { get; } = [];

    public void ProcessNextToken(IToken token)
    {
        switch (token)
        {
            case WordToken wt when CurrentResult == 0:
            {
                var result = wt.Value.TrimEnd(':');
                CurrentResult = BigInteger.Parse(result);
                break;
            }
            case IntToken it:
            {
                CurrentNumbers.Add((BigInteger)it.Value);
                break;
            }
            case EndOfLineToken:
                Equations.Add(new CalibrationEquation(CurrentResult, CurrentNumbers.ToArray()));
                CurrentResult = 0;
                CurrentNumbers.Clear();
                break;
        }
    }

    public void Finish()
    {
        Equations.Add(new CalibrationEquation(CurrentResult, CurrentNumbers.ToArray()));
        var res = puzzle.Solve(Equations);
        Console.WriteLine(res);
    }
}