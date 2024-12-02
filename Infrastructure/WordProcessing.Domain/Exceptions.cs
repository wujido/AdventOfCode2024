using System;

namespace WordProcessing.Domain;

public abstract class WordProcessingException(string message) : ApplicationException(message);

public class FileErrorException() : WordProcessingException("File Error");