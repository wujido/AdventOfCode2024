using WordProcessing.Domain.Puzzles;

namespace Day2;

internal class Part1 : ILinesPuzzle
{
    public int Solve(List<List<int>> reports)
    {
        return reports.Count(IsSafe);
    }

    private bool IsSafe(List<int> report)
    {
        var isIncreasing = report[0] < report[1];
        var previousLevel = report[0];

        foreach (var level in report.Skip(1))
        {
            var diff = level - previousLevel;

            if (diff == 0) return false;

            switch (isIncreasing)
            {
                case true when diff is < 1 or > 3:
                case false when diff is > -1 or < -3:
                    return false;
            }

            previousLevel = level;
        }

        return true;
    }
}