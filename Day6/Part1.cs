using WordProcessing.Domain.Puzzles;

namespace Day6;

internal class Part1 : IMatrixPuzzle<char>
{
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public int Solve(List<List<char>> matrix)
    {
        var guardPosition = FindGuardPosition(matrix);
        var direction = Direction.Up;
        var visited = new HashSet<(int x, int y)>();

        try
        {
            while (IsInMatrix(matrix, guardPosition))
            {
                visited.Add(guardPosition);
                (int X, int Y) nextPosition;
                switch (direction)
                {
                    case Direction.Up:
                        nextPosition = (guardPosition.X, guardPosition.Y - 1);
                        if (matrix[nextPosition.Y][nextPosition.X] == '#')
                        {
                            direction = Direction.Right;
                        }
                        else
                        {
                            guardPosition = nextPosition;
                        }

                        break;
                    case Direction.Down:
                        nextPosition = (guardPosition.X, guardPosition.Y + 1);
                        if (matrix[nextPosition.Y][nextPosition.X] == '#')
                        {
                            direction = Direction.Left;
                        }
                        else
                        {
                            guardPosition = nextPosition;
                        }

                        break;
                    case Direction.Left:
                        nextPosition = (guardPosition.X - 1, guardPosition.Y);
                        if (matrix[nextPosition.Y][nextPosition.X] == '#')
                        {
                            direction = Direction.Up;
                        }
                        else
                        {
                            guardPosition = nextPosition;
                        }

                        break;
                    case Direction.Right:
                        nextPosition = (guardPosition.X + 1, guardPosition.Y);
                        if (matrix[nextPosition.Y][nextPosition.X] == '#')
                        {
                            direction = Direction.Down;
                        }
                        else
                        {
                            guardPosition = nextPosition;
                        }

                        break;
                    default:
                        throw new InvalidOperationException("Invalid direction");
                }
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            return visited.Count;
        }


        return visited.Count;
    }

    private static bool IsInMatrix(List<List<char>> matrix, (int X, int Y) position)
    {
        return position.X >= 0 && position.X < matrix[0].Count && position.Y >= 0 & position.Y < matrix[0].Count;
    }

    private static (int X, int Y) FindGuardPosition(List<List<char>> matrix)
    {
        for (var y = 0; y < matrix.Count; y++)
        {
            for (var x = 0; x < matrix[0].Count; x++)
            {
                if (matrix[y][x] == '^')
                {
                    return (x, y);
                }
            }
        }

        return (-1, -1);
    }
}