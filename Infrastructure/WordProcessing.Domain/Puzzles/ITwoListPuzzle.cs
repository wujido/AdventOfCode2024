namespace WordProcessing.Domain.Puzzles;

public interface ITwoListPuzzle
{
    int Solve(List<int> left, List<int> right);
}