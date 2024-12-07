using WordProcessing.App;

var appErrorHandler = new AppErrorHandler(Console.Error);
appErrorHandler.RunProgram(
    new AoCIntProcessingProgram(
        new Day7Analyzer(new Part1()),
        new Day7Analyzer(new Part2())
    ),
    args
);