namespace Tests.Day4;

public class Part1Tests
{
    [Fact]
    public void NoXmas_ResultIsZero()
    {
        List<string> input = ["..X.."];

        RunXMASText(input, 0);
    }

    [Fact]
    public void OneXmasInLine_CorrectCount()
    {
        List<string> input = ["XMAS"];

        RunXMASText(input, 1);
    }

    [Fact]
    public void XmasOnMultipleLines_CorrectCount()
    {
        List<string> input =
        [
            "XMAS",
            "XMAS"
        ];

        RunXMASText(input, 2);
    }

    [Fact]
    public void IncompleteXmas_CorrectCount()
    {
        List<string> input =
        [
            "XXMAS",
            "XMXMAS",
            "XMAXMAS"
        ];

        RunXMASText(input, 3);
    }

    [Fact]
    public void XmasSplitOnMultipleLines_IsNotCounted()
    {
        List<string> input =
        [
            "XM",
            "AS",
        ];

        RunXMASText(input, 0);
    }

    [Fact]
    public void ReversedXmas_CorrectCount()
    {
        List<string> input = ["SAMX"];

        RunXMASText(input, 1);
    }

    [Fact]
    public void ReversedXmasSplitAcrossMultipleLines_IsNotCounted()
    {
        List<string> input =
        [
            "SA",
            "MX",
            "MX",
            "SA",
        ];

        RunXMASText(input, 0);
    }

    [Fact]
    public void VerticalXmas_CorrectCount()
    {
        List<string> input =
        [
            "X",
            "M",
            "A",
            "S",
        ];

        RunXMASText(input, 1);
    }

    [Fact]
    public void ReversedVerticalXmas_CorrectCount()
    {
        List<string> input =
        [
            "S",
            "A",
            "M",
            "X",
        ];

        RunXMASText(input, 1);
    }

    [Fact]
    public void DiagonalLeftToRight_CorrectCount()
    {
        List<string> input =
        [
            "X.....",
            "XMX...",
            ".MAM..",
            "..ASA.",
            "...S.S",
        ];

        RunXMASText(input, 3);
    }

    [Fact]
    public void ReversedDiagonalLeftToRight_CorrectCount()
    {
        List<string> input =
        [
            "S.....",
            "SAS...",
            ".AMA..",
            "..MXM.",
            "...X.X",
        ];

        RunXMASText(input, 3);
    }

    [Fact]
    public void DiagonalRightToLeft_CorrectCount()
    {
        List<string> input =
        [
            ".....X",
            "...XMX",
            "..MAM.",
            ".ASA..",
            "S.S...",
        ];

        RunXMASText(input, 3);
    }

    [Fact]
    public void ReversedDiagonalRightToLeft_CorrectCount()
    {
        List<string> input =
        [
            ".....S",
            "...SAS",
            "..AMA.",
            ".MXM..",
            "X.X...",
        ];

        RunXMASText(input, 3);
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

        RunXMASText(input, 18);
    }


    // ReSharper disable once InconsistentNaming
    private void RunXMASText(List<string> input, int count)
    {
        var finalInput = input.Select(x => x.ToCharArray().ToList()).ToList();

        var puzzle = new global::Day4.Part1();
        var res = puzzle.Solve(finalInput);

        Assert.Equal(count, res);
    }
}