using System.IO;
using WordProcessing.Domain;

namespace WordProcessing.App;

public class AppErrorHandler(TextWriter errorWriter)
{
    private TextWriter ErrorWriter { get; set; } = errorWriter;

    public void RunProgram(IProgramCore program, string[] args)
    {
        try
        {
            program.Run(args);
        }
        catch (WordProcessingException e)
        {
            ErrorWriter.WriteLine(e.Message);
        }
    }
}