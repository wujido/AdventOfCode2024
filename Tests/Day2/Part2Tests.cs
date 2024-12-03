using Day2;

namespace Tests.Day2;

public class Part2Tests
{
    [Fact]
    public void AllIncreaseByOne_IsSafe()
    {
        AssertReportIsSafe([1, 2, 3, 4]);
    }

    [Fact]
    public void AllDecreaseByOne_IsSafe()
    {
        AssertReportIsSafe([4, 3, 2, 1]);
    }

    [Fact]
    public void AllIncreaseByAllowedRange_IsSafe()
    {
        AssertReportIsSafe([1, 2, 4, 7, 9, 10]);
    }

    [Fact]
    public void AllDecreaseByAllowedRange_IsSafe()
    {
        AssertReportIsSafe([10, 9, 7, 4, 2, 1]);
    }

    [Fact]
    public void TwoEquals_AtTheBeginning_IsSafe()
    {
        AssertReportIsSafe([1, 1, 2, 3, 4]);
        AssertReportIsSafe([4, 4, 3, 2, 1]);
    }

    [Fact]
    public void TwoEquals_InTheMiddle_IsSafe()
    {
        AssertReportIsSafe([1, 2, 2, 3, 4]);
        AssertReportIsSafe([1, 2, 3, 3, 4]);

        AssertReportIsSafe([4, 3, 3, 2, 1]);
        AssertReportIsSafe([4, 3, 2, 2, 1]);
    }

    [Fact]
    public void TwoEquals_AtTheEnd_IsSafe()
    {
        AssertReportIsSafe([1, 2, 3, 4, 4]);
        AssertReportIsSafe([4, 3, 2, 1, 1]);
    }

    [Fact]
    public void FirstGapIncrease_ThenAllDecrease_IsSafe()
    {
        AssertReportIsSafe([4, 5, 3, 2, 1]);
    }

    [Fact]
    public void FirstGapDecrease_ThenIncrease_IsSafe()
    {
        AssertReportIsSafe([5, 4, 6, 7, 8]);
    }

    [Fact]
    public void OneMonotonyChange_InTheMiddle_IsSafe()
    {
        AssertReportIsSafe([1, 2, 1, 3, 4]);
        AssertReportIsSafe([4, 3, 4, 2, 1]);
    }

    [Fact]
    public void ThreeSame_UnSafe()
    {
        AssertReportIsUnSafe([1, 1, 1]);
    }

    [Fact]
    public void TwoMonotonyChange_InTheMiddle_UnSafe()
    {
        AssertReportIsUnSafe([1, 2, 1, 3, 1, 4]);
        AssertReportIsUnSafe([4, 3, 4, 2, 4, 1]);
    }

    [Fact]
    public void SingleHighIncrease_InIncreasing_UnSafe()
    {
        AssertReportIsUnSafe([1, 2, 8, 9, 10]);
    }

    [Fact]
    public void SingleHighDecrease_InDecreasing_UnSafe()
    {
        AssertReportIsUnSafe([10, 9, 2, 1, 0]);
    }

    [Fact]
    public void SingleHighJump_AtTheBeginning_IsSafe()
    {
        AssertReportIsSafe([1, 6, 7, 8]);
        AssertReportIsSafe([8, 3, 2, 1]);
    }

    [Fact]
    public void SingleHighJump_AtTheEnd_IsSafe()
    {
        AssertReportIsSafe([1, 2, 3, 8]);
        AssertReportIsSafe([8, 7, 6, 1]);
    }

    [Fact]
    public void SingleSpike_InTheMiddle_IsSafe()
    {
        AssertReportIsSafe([1, 2, 8, 3, 4]);
        AssertReportIsSafe([8, 7, 1, 6, 5]);
    }


    [Fact]
    public void ExamplesFromAssignment()
    {
        AssertReportIsSafe([7, 6, 4, 2, 1]);
        AssertReportIsUnSafe([1, 2, 7, 8, 9]);
        AssertReportIsUnSafe([9, 7, 6, 2, 1]);
        AssertReportIsSafe([1, 3, 2, 4, 5]);
        AssertReportIsSafe([8, 6, 4, 4, 1]);
        AssertReportIsSafe([1, 3, 6, 7, 9]);

    }



    private static void AssertReportIsSafe(List<int> input)
    {
        var puzzle = new Part2();
        var res = puzzle.Solve([input]);
        Assert.Equal(1, res);
    }

    private static void AssertReportIsUnSafe(List<int> input)
    {
        var puzzle = new Part2();
        var res = puzzle.Solve([input]);
        Assert.Equal(0, res);
    }
}