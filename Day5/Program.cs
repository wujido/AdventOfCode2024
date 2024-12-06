using WordProcessing.App;
using WordProcessing.Domain.TokenAnalytics;
using WordProcessing.Domain.TokenReading;

var appErrorHandler = new AppErrorHandler(Console.Error);
appErrorHandler.RunProgram(
    new AoCParagraphAwareProgram(
        new Part1(),
        new Part2()
    ),
    args
);

internal abstract class Day5Analyzer : ITokenAnalyzer
{
    protected Dictionary<int, HashSet<int>> Rules { get; } = [];
    private bool ReadingRules { get; set; } = true;

    protected List<int[]> PageOrderings { get; } = [];

    public void ProcessNextToken(IToken token)
    {
        switch (token)
        {
            case WordToken word when ReadingRules:
                AddRule(word.Value);
                break;
            case WordToken word when !ReadingRules:
                AnalyzePageOrdering(word.Value);
                break;
            case EndOfParagraphToken:
                ReadingRules = false;
                break;
        }
    }

    private void AddRule(string rule)
    {
        var parts = rule.Split('|').Select(int.Parse).ToArray();

        if (Rules.TryGetValue(parts[1], out var rules))
        {
            rules.Add(parts[0]);
        }
        else
        {
            rules = [parts[0]];
        }

        Rules[parts[1]] = rules;
    }

    private void AnalyzePageOrdering(string pageOrder)
    {
        var pages = pageOrder.Split(',').Select(int.Parse).ToArray();
        PageOrderings.Add(pages);
    }

    protected (List<int[]> Correct, List<int[]> Incorrect) AnalyzePageOrder(List<int[]> pageOrderings)
    {
        List<int[]> correct = [];
        List<int[]> incorrect = [];

        foreach (var ordering in pageOrderings)
        {
            var isCorrect = true;
            var bannedNumbers = new HashSet<int>();

            foreach (var page in ordering)
            {
                if (bannedNumbers.Contains(page))
                {
                    isCorrect = false;
                    break;
                }

                if (Rules.TryGetValue(page, out var toBan))
                {
                    bannedNumbers.UnionWith(toBan);
                }
            }

            if (isCorrect)
            {
                correct.Add(ordering);
            }
            else
            {
                incorrect.Add(ordering);
            }
        }

        return (correct, incorrect);
    }

    public abstract void Finish();
}

internal class Part1 : Day5Analyzer
{
    public override void Finish()
    {
        var (correct, _) = AnalyzePageOrder(PageOrderings);

        var sumOfMiddle = correct.Sum(ordering => ordering[ordering.Length / 2]);
        Console.WriteLine(sumOfMiddle);
    }
}

internal class Part2 : Day5Analyzer
{
    public override void Finish()
    {
        var (_, incorrect) = AnalyzePageOrder(PageOrderings);

        var corrected = new List<int[]>();
        while (incorrect.Count > 0)
        {
            // Rules: a [všechny b co musí být před a]
            foreach (var ordering in incorrect)
            {
                for (var i = 0; i < ordering.Length; i++)
                {
                    var page = ordering[i];

                    if (!Rules.TryGetValue(page, out var hasToBeBeforePage)) continue;

                    var violations = ordering
                        .Skip(i)
                        .Where(x => hasToBeBeforePage.Contains(x))
                        .ToArray();

                    if (violations.Length == 0) continue;

                    var toSwitch = violations
                        .Select(x => Array.IndexOf(ordering, x))
                        .Max();

                    (ordering[i], ordering[toSwitch]) = (ordering[toSwitch], ordering[i]);
                    break;
                }
            }

            var (ok, fail) = AnalyzePageOrder(incorrect);
            corrected.AddRange(ok);

            foreach (var o in ok)
            {
                incorrect.Remove(o);
            }
        }

        var sumOfMiddle = corrected.Sum(ordering => ordering[ordering.Length / 2]);
        Console.WriteLine(sumOfMiddle);
    }
}

public static class ArrayExtensions
{
    public static void Shuffle<T>(this T[] array)
    {
        var random = new Random();
        for (var i = array.Length - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]); // Swap elements
        }
    }
}