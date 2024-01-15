namespace Froggie.Domain.Users;

[ExcludeFromCodeCoverage]
public sealed class NameIsTakenException : Exception
{
    public string NameValue { get; }

    public NameIsTakenException(string nameValue)
    {
        NameValue = nameValue;
    }

    public NameIsTakenException(string? message, string nameValue)
        : base(message)
    {
        NameValue = nameValue;
    }

    public NameIsTakenException(string? message, Exception? innerException, string nameValue)
        : base(message, innerException)
    {
        NameValue = nameValue;
    }
}