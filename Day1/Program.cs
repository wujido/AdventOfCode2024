using Day1;

using var reader = new StreamReader(args[1]);

IPuzzle puzzle = args[0] == "1" ? new Part1() : new Part2();

var left = new List<int>();
var right = new List<int>();

while (reader.ReadLine() is { } line)
{
    var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    left.Add(int.Parse(parts[0]));
    right.Add(int.Parse(parts[1]));
}

var res = puzzle.Solve(left, right);
Console.WriteLine(res);