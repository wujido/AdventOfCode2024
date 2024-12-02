namespace WordProcessing.Domain.TokenReading;


public interface IToken;

public readonly record struct WordToken(string Value) : IToken;
public readonly record struct IntToken(int Value) : IToken;
public readonly record struct EndOfInputToken : IToken;
public readonly record struct EndOfLineToken : IToken;
public readonly record struct EndOfParagraphToken : IToken;
