using WordProcessing.Domain.Puzzles;

namespace Day1;

/// <summary>
/// Pair up the smallest number in the left list with the smallest number in the right list,
/// then the second-smallest left number with the second-smallest right number, and so on.
///
/// Within each pair, figure out how far apart the two numbers are; you'll need to add up all of those distances.
/// For example, if you pair up a 3 from the left list with a 7 from the right list, the distance apart is 4;
/// if you pair up a 9 with a 3, the distance apart is 6.
/// </summary>
internal class Part1 : ITwoListPuzzle
{
    public int Solve(List<int> left, List<int> right)
    {
        return left.Order()
            .Zip(
                right.Order(),
                (x, y) => Math.Abs(x - y)
            )
            .Sum();
    }
}