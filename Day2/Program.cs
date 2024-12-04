using System.Runtime.CompilerServices;
using Day2;
using WordProcessing.App;
using WordProcessing.Domain.TokenAnalytics;

var appErrorHandler = new AppErrorHandler(Console.Error);
appErrorHandler.RunProgram(
    new AoCIntProcessingProgram(
        new IntMatrixAnalyzer(new Part1()),
        new IntMatrixAnalyzer(new Part2())
    ),
    args
);