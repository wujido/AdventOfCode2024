using WordProcessing.Domain.Puzzles;

namespace Day1;

/// <summary>
/// This time, you'll need to figure out exactly how often each number from the left list appears in the right list.
/// Calculate a total similarity score by adding up each number in the left list
/// after multiplying it by the number of times that number appears in the right list.
/// </summary>
internal class Part2 : ITwoListPuzzle
{
    public int Solve(List<int> left, List<int> right)
    {
        var groups = right
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());

        var sum = 0;
        foreach (var a in left)
        {
            groups.TryGetValue(a, out var b);
            sum += a * b;
        }

        return sum;
    }
}