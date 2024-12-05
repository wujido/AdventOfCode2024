using System.Collections;
using WordProcessing.Domain.Puzzles;

namespace Day4;

public class Part1 : IMatrixPuzzle<char>
{
    public int Solve(List<List<char>> matrix)
    {
        var stateMachine = new XmasStateMachine();

        IEnumerable<IEnumerable<char>> strategies =
        [
            new HorizontalStrategy(matrix),
            new ReverseHorizontalStrategy(matrix),
            new VerticalStrategy(matrix),
            new ReversedVerticalStrategy(matrix),
            new DiagonalLeftToRightStrategy(matrix),
            new ReversedDiagonalLeftToRightStrategy(matrix),
            new DiagonalRightToLeftStrategy(matrix),
            new ReversedDiagonalRightToLeftStrategy(matrix)
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

internal class XmasStateMachine
{
    private enum State
    {
        Initial,
        GotX,
        GotM,
        GotA,
    }

    private State CurrentState { get; set; }
    public int XmasCount { get; private set; }

    public void ProcessNextChar(char ch)
    {
        switch (CurrentState)
        {
            case State.Initial:
                if (ch is 'X') CurrentState = State.GotX;
                break;
            case State.GotX:
                CurrentState = ch switch
                {
                    'X' => State.GotX,
                    'M' => State.GotM,
                    _ => State.Initial
                };

                break;
            case State.GotM:
                CurrentState = ch switch
                {
                    'X' => State.GotX,
                    'A' => State.GotA,
                    _ => State.Initial
                };
                break;
            case State.GotA:
                switch (ch)
                {
                    case 'X':
                        CurrentState = State.GotX;
                        break;
                    case 'S':
                        XmasCount++;
                        CurrentState = State.Initial;
                        break;
                    default:
                        CurrentState = State.Initial;
                        break;
                }

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

internal class HorizontalStrategy(List<List<char>> matrix) : IEnumerable<char>
{
    public IEnumerator<char> GetEnumerator()
    {
        return matrix.SelectMany(row => row.Concat(['\n'])).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class ReverseHorizontalStrategy(List<List<char>> matrix) : IEnumerable<char>
{
    public IEnumerator<char> GetEnumerator()
    {
        return matrix.SelectMany(
            row => row.AsEnumerable().Reverse().Concat(['\n'])
        ).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class VerticalStrategy(List<List<char>> matrix) : IEnumerable<char>
{
    public IEnumerator<char> GetEnumerator()
    {
        for (var x = 0; x < matrix[0].Count; x++)
        {
            foreach (var row in matrix)
            {
                yield return row[x];
            }

            yield return '\n';
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class ReversedVerticalStrategy(List<List<char>> matrix) : IEnumerable<char>
{
    public IEnumerator<char> GetEnumerator()
    {
        for (var x = 0; x < matrix[0].Count; x++)
        {
            for (var index = matrix.Count - 1; index >= 0; index--)
            {
                var row = matrix[index];
                yield return row[x];
            }

            yield return '\n';
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class DiagonalLeftToRightStrategy(List<List<char>> matrix) : IEnumerable<char>
{
    public IEnumerator<char> GetEnumerator()
    {
        var rows = matrix.Count;
        var cols = matrix[0].Count;
        var result = new List<char>();

        // Start from the left bottom corner and move upwards diagonally
        for (int startRow = rows - 1; startRow >= 0; startRow--)
        {
            var diagonal = GetDiagonal(startRow, 0);
            if (diagonal.Count >= 4)
            {
                result.AddRange(diagonal);
                result.Add('\n'); // Add newline after each diagonal
            }
        }

        // Continue from the first column (excluding the bottom-left corner already processed)
        for (int startCol = 1; startCol < cols; startCol++)
        {
            var diagonal = GetDiagonal(0, startCol);
            if (diagonal.Count >= 4)
            {
                result.AddRange(diagonal);
                result.Add('\n'); // Add newline after each diagonal
            }
        }

        return result.GetEnumerator();
    }

    private List<char> GetDiagonal(int startRow, int startCol)
    {
        var diagonal = new List<char>();
        var row = startRow;
        var col = startCol;
        var rows = matrix.Count;
        var cols = matrix[0].Count;

        // Traverse the diagonal starting from (startRow, startCol)
        while (row < rows && col < cols)
        {
            diagonal.Add(matrix[row][col]);
            row++;
            col++;
        }

        return diagonal;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class ReversedDiagonalLeftToRightStrategy(List<List<char>> matrix) : IEnumerable<char>
{
    public IEnumerator<char> GetEnumerator()
    {
        var rows = matrix.Count;
        var cols = matrix[0].Count;
        var result = new List<char>();

        // Start from the left bottom corner and move upwards diagonally in reverse order
        for (int startRow = rows - 1; startRow >= 0; startRow--)
        {
            var diagonal = GetDiagonal(startRow, 0);
            if (diagonal.Count >= 4)
            {
                diagonal.Reverse();
                result.AddRange(diagonal);
                result.Add('\n'); // Add newline after each diagonal
            }
        }

        // Continue from the first column (excluding the bottom-left corner already processed)
        for (int startCol = 1; startCol < cols; startCol++)
        {
            var diagonal = GetDiagonal(0, startCol);
            if (diagonal.Count >= 4)
            {
                diagonal.Reverse();
                result.AddRange(diagonal);
                result.Add('\n'); // Add newline after each diagonal
            }
        }

        return result.GetEnumerator();
    }

    private List<char> GetDiagonal(int startRow, int startCol)
    {
        var diagonal = new List<char>();
        var row = startRow;
        var col = startCol;
        var rows = matrix.Count;
        var cols = matrix[0].Count;

        // Traverse the diagonal starting from (startRow, startCol)
        while (row < rows && col < cols)
        {
            diagonal.Add(matrix[row][col]);
            row++;
            col++;
        }

        return diagonal;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class DiagonalRightToLeftStrategy(List<List<char>> matrix) : IEnumerable<char>
{
    public IEnumerator<char> GetEnumerator()
    {
        var rows = matrix.Count;
        var cols = matrix[0].Count;
        var result = new List<char>();

        // Start from the top-right corner and move downwards diagonally to the bottom-left
        for (int startCol = cols - 1; startCol >= 0; startCol--)
        {
            var diagonal = GetDiagonal(0, startCol);
            if (diagonal.Count >= 4)
            {
                result.AddRange(diagonal);
                result.Add('\n'); // Add newline after each diagonal
            }
        }

        // Continue from the second row (excluding the top-right corner already processed)
        for (int startRow = 1; startRow < rows; startRow++)
        {
            var diagonal = GetDiagonal(startRow, cols - 1);
            if (diagonal.Count >= 4)
            {
                result.AddRange(diagonal);
                result.Add('\n'); // Add newline after each diagonal
            }
        }

        return result.GetEnumerator();
    }

    private List<char> GetDiagonal(int startRow, int startCol)
    {
        var diagonal = new List<char>();
        var row = startRow;
        var col = startCol;

        // Traverse the diagonal starting from (startRow, startCol)
        while (row < matrix.Count && col >= 0)
        {
            diagonal.Add(matrix[row][col]);
            row++;
            col--;
        }

        return diagonal;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class ReversedDiagonalRightToLeftStrategy(List<List<char>> matrix) : IEnumerable<char>
{
    public IEnumerator<char> GetEnumerator()
    {
        var rows = matrix.Count;
        var cols = matrix[0].Count;
        var result = new List<char>();

        // Start from the top-right corner and move downwards diagonally to the bottom-left in reversed order
        for (int startCol = cols - 1; startCol >= 0; startCol--)
        {
            var diagonal = GetDiagonal(0, startCol);
            if (diagonal.Count >= 4)
            {
                diagonal.Reverse();
                result.AddRange(diagonal);
                result.Add('\n'); // Add newline after each diagonal
            }
        }

        // Continue from the second row (excluding the top-right corner already processed) in reversed order
        for (int startRow = 1; startRow < rows; startRow++)
        {
            var diagonal = GetDiagonal(startRow, cols - 1);
            if (diagonal.Count >= 4)
            {
                diagonal.Reverse();
                result.AddRange(diagonal);
                result.Add('\n'); // Add newline after each diagonal
            }
        }

        return result.GetEnumerator();
    }

    private List<char> GetDiagonal(int startRow, int startCol)
    {
        var diagonal = new List<char>();
        var row = startRow;
        var col = startCol;

        // Traverse the diagonal starting from (startRow, startCol)
        while (row < matrix.Count && col >= 0)
        {
            diagonal.Add(matrix[row][col]);
            row++;
            col--;
        }

        return diagonal;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
