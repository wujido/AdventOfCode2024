namespace Tests.Day4;

public class Part2Tests
{
    [Fact]
    public void SingleXV1_CorrectCount()
    {
        List<string> input =
        [
            "M.S",
            ".A.",
            "M.S "
        ];

        RunXMASText(input, 1);
    }

    [Fact]
    public void SingleXV2_CorrectCount()
    {
        List<string> input =
        [
            "M.M",
            ".A.",
            "S.S "
        ];

        RunXMASText(input, 1);
    }

    [Fact]
    public void SingleXV3_CorrectCount()
    {
        List<string> input =
        [
            "S.S",
            ".A.",
            "M.M "
        ];

        RunXMASText(input, 1);
    }

    [Fact]
    public void SingleXV4_CorrectCount()
    {
        List<string> input =
        [
            "S.M",
            ".A.",
            "S.M "
        ];

        RunXMASText(input, 1);
    }

    [Fact]
    public void ExampleFromAoC_CorrectCount()
    {
        List<string> input =
        [
            "MMMSXXMASM",
            "MSAMXMSMSA",
            "AMXSXMAAMM",
            "MSAMASMSMX",
            "XMASAMXAMM",
            "XXAMMXXAMA",
            "SMSMSASXSS",
            "SAXAMASAAA",
            "MAMMMXMMMM",
            "MXMXAXMASX"
        ];

        RunXMASText(input, 9);
    }

    // ReSharper disable once InconsistentNaming
    private void RunXMASText(List<string> input, int count)
    {
        var finalInput = input.Select(x => x.ToCharArray().ToList()).ToList();

        var puzzle = new global::Day4.Part2();
        var res = puzzle.Solve(finalInput);

        Assert.Equal(count, res);
    }
}