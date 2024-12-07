using System.Numerics;

internal class Part1 : IDay7Puzzle
{
    public BigInteger Solve(List<CalibrationEquation> equations)
    {
        List<BigInteger> calibratable = [];
        foreach (var equation in equations)
        {
            var (result, numbers) = equation;

            var results = ComputeAllResults(numbers);

            if (results.Contains(result))
                calibratable.Add(result);
        }

        BigInteger final = 0;
        foreach (var c in calibratable)
        {
            final += c;
        }

        return final;
    }

    static List<BigInteger> ComputeAllResults(BigInteger[] numbers)
    {
        var results = new List<BigInteger>();
        ComputeResultsRecursive(numbers, 0, numbers[0], results);
        return results;
    }

    static void ComputeResultsRecursive(BigInteger[] numbers, int index, BigInteger currentResult,
        List<BigInteger> results)
    {
        if (index == numbers.Length - 1)
        {
            results.Add(currentResult);
            return;
        }

        // Add the next number with '+'
        ComputeResultsRecursive(numbers, index + 1, currentResult + numbers[index + 1], results);

        // Multiply the next number with '*'
        ComputeResultsRecursive(numbers, index + 1, currentResult * numbers[index + 1], results);
    }
}