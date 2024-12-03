using Day1;
using WordProcessing.App;
using WordProcessing.Domain.TokenAnalytics;

var appErrorHandler = new AppErrorHandler(Console.Error);
appErrorHandler.RunProgram(
    new AoCIntProcessingProgram(
        new TwoColumnsAnalyzer(new Part1()),
        new TwoColumnsAnalyzer(new Part2())
    ),
    args
);