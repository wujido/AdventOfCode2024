using System.Collections;
using WordProcessing.Domain.Puzzles;

namespace Day4;

public class Part2 : IMatrixPuzzle<char>
{
    public int Solve(List<List<char>> matrix)
    {
        var stateMachine = new XMasMasStateMachine();
        IEnumerable<IEnumerable<char>> strategies =
        [
            new XShapeDiagonalStrategy1(matrix),
            new XShapeDiagonalStrategy2(matrix),
            new XShapeDiagonalStrategy3(matrix),
            new XShapeDiagonalStrategy4(matrix)
        ];

        foreach (var strategy in strategies)
        {
            foreach (var ch in strategy)
            {
                stateMachine.ProcessNextChar(ch);
            }

            stateMachine.Reset();
        }


        return stateMachine.XmasCount;
    }
}

internal class XMasMasStateMachine
{
    private enum State
    {
        Initial,
        GotM,
        GotMA,
        GotMAS,
        GotMASM,
        GotMASMA,
    }

    private State CurrentState { get; set; }
    public int XmasCount { get; private set; }

    public void ProcessNextChar(char ch)
    {
        switch (CurrentState)
        {
            case State.Initial:
                if (ch is 'M') CurrentState = State.GotM;
                break;
            case State.GotM:
                CurrentState = ch switch
                {
                    'A' => State.GotMA,
                    _ => State.Initial
                };

                break;
            case State.GotMA:
                CurrentState = ch switch
                {
                    'S' => State.GotMAS,
                    _ => State.Initial
                };
                break;
            case State.GotMAS:
                CurrentState = ch switch
                {
                    'M' => State.GotMASM,
                    _ => State.Initial
                };
                break;
            case State.GotMASM:
                CurrentState = ch switch
                {
                    'A' => State.GotMASMA,
                    _ => State.Initial
                };
                break;
            case State.GotMASMA:
                if (ch == 'S') XmasCount++;

                CurrentState = State.Initial;
                break;
            default:
                throw new InvalidOperationException("Invalid state");
        }
    }

    public void Reset()
    {
        CurrentState = State.Initial;
    }
}

internal abstract class XShapeDiagonalStrategy(List<List<char>> matrix) : IEnumerable<char>
{
    public IEnumerator<char> GetEnumerator()
    {
        var rows = matrix.Count;
        var cols = matrix[0].Count;
        var result = new List<char>();

        for (int i = 0; i <= rows - 3; i++)
        {
            for (int j = 0; j <= cols - 3; j++)
            {
                var xShape = GetXShape(i, j);
                result.AddRange(xShape);
                result.Add('\n'); // Add newline after each X
            }
        }

        return result.GetEnumerator();
    }

    protected abstract List<char> GetXShape(int startRow, int startCol);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class XShapeDiagonalStrategy1(List<List<char>> matrix) : XShapeDiagonalStrategy(matrix)
{
    protected override List<char> GetXShape(int startRow, int startCol)
    {
        var xShape = new List<char>
        {
            matrix[startRow][startCol], // Top-left
            matrix[startRow + 1][startCol + 1], // Center
            matrix[startRow + 2][startCol + 2], // Bottom-right
            matrix[startRow + 2][startCol], // Bottom-left
            matrix[startRow + 1][startCol + 1], // Center
            matrix[startRow][startCol + 2] // Top-right
        };

        return xShape;
    }
}

internal class XShapeDiagonalStrategy2(List<List<char>> matrix) : XShapeDiagonalStrategy(matrix)
{
    protected override List<char> GetXShape(int startRow, int startCol)
    {
        var xShape = new List<char>
        {
            matrix[startRow][startCol], // Top-left
            matrix[startRow + 1][startCol + 1], // Center
            matrix[startRow + 2][startCol + 2], // Bottom-right

            matrix[startRow][startCol + 2], // Top-right
            matrix[startRow + 1][startCol + 1], // Center
            matrix[startRow + 2][startCol], // Bottom-left
        };

        return xShape;
    }
}

internal class XShapeDiagonalStrategy3(List<List<char>> matrix) : XShapeDiagonalStrategy(matrix)
{
    protected override List<char> GetXShape(int startRow, int startCol)
    {
        var xShape = new List<char>
        {
            matrix[startRow + 2][startCol + 2], // Bottom-right
            matrix[startRow + 1][startCol + 1], // Center
            matrix[startRow][startCol], // Top-left

            matrix[startRow + 2][startCol], // Bottom-left
            matrix[startRow + 1][startCol + 1], // Center
            matrix[startRow][startCol + 2] // Top-right
        };

        return xShape;
    }
}
internal class XShapeDiagonalStrategy4(List<List<char>> matrix) : XShapeDiagonalStrategy(matrix)
{
    protected override List<char> GetXShape(int startRow, int startCol)
    {
        var xShape = new List<char>
        {
            matrix[startRow + 2][startCol + 2], // Bottom-right
            matrix[startRow + 1][startCol + 1], // Center
            matrix[startRow][startCol], // Top-left

            matrix[startRow][startCol + 2], // Top-right
            matrix[startRow + 1][startCol + 1], // Center
            matrix[startRow + 2][startCol], // Bottom-left
        };

        return xShape;
    }
}
