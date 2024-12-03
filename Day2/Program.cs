using System.Runtime.CompilerServices;
using Day2;
using WordProcessing.App;
using WordProcessing.Domain.TokenAnalytics;

var appErrorHandler = new AppErrorHandler(Console.Error);
appErrorHandler.RunProgram(
    new AoCProgram(
        new LinesAnalyzer(new Part1()),
        new LinesAnalyzer(new Part2())
    ),
    args
);