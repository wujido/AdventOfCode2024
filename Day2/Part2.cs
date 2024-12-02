using WordProcessing.Domain.Puzzles;

namespace Day2;

internal class Part2 : ILinesPuzzle
{
    public int Solve(List<List<int>> reports)
    {
        return reports.Count(IsSafeWithDampener);
    }

    private static bool IsSafeWithDampener(List<int> report)
    {
        // Check if the report is safe without any modifications
        if (IsSafe(report))
        {
            return true;
        }

        // Check if skipping one element makes the report safe
        return report.Select(
                (_, i) => report.Where((_, index) => index != i).ToList()
            )
            .Any(IsSafe);
    }

    private static bool IsSafe(List<int> report)
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