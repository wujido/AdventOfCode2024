using WordProcessing.Domain;

namespace WordProcessing.IO;

public class InputOutputState(string[] args) : IDisposable
{
    public TextReader? Reader { get; set; }
    public TextWriter? Writer { get; set; }
    private string[] Args { get; set; } = args;


    public void OpenInputFile(int argIndex)
    {
        try
        {
            Reader = new StreamReader(Args[argIndex]);
        }
        catch (Exception e)
            when (e is IOException or UnauthorizedAccessException or ArgumentException)
        {
            throw new FileErrorException();
        }
    }


    public void OpenOutputFile(int argIndex)
    {
        try
        {
            Writer = new StreamWriter(Args[argIndex]);
        }
        catch (Exception e)
            when (e is IOException or UnauthorizedAccessException or ArgumentException)
        {
            throw new FileErrorException();
        }
    }


    public void Dispose()
    {
        Reader?.Dispose();
        Writer?.Dispose();
    }
}