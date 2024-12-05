using Day4;
using WordProcessing.App;
using WordProcessing.Domain.TokenAnalytics;

var appErrorHandler = new AppErrorHandler(Console.Error);
appErrorHandler.RunProgram(
    new AoCIntProcessingProgram(
        new CharMatrixAnalyzer(new Part1()),
        new CharMatrixAnalyzer(new Part2())
    ),
    args
);