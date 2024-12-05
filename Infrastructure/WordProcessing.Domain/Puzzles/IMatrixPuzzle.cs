namespace WordProcessing.Domain.Puzzles;

public interface IMatrixPuzzle<TItem>
{
    int Solve(List<List<TItem>> matrix);
}