namespace WordProcessing.Domain.Puzzles;

public interface ILinesPuzzle
{
    int Solve(List<List<int>> lines);
}