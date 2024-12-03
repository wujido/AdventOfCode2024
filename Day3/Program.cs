using Day3;
using WordProcessing.App;

var appErrorHandler = new AppErrorHandler(Console.Error);
appErrorHandler.RunProgram(
    new AoCProgram(
        new Day3AnalyzerPart1(),
        new Day3AnalyzerPart2()
    ),
    args
);