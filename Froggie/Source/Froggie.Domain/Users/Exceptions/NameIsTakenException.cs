using System.Runtime.Serialization;

namespace Froggie.Domain.Users;

public sealed class NameIsTakenException : Exception
{
    public string NameValue { get; }

    public NameIsTakenException(string nameValue)
    {
        NameValue = nameValue;
    }

    public NameIsTakenException(SerializationInfo info, StreamingContext context, string nameValue) : base(info,
        context)
    {
        NameValue = nameValue;
    }

    public NameIsTakenException(string? message, string nameValue) : base(message)
    {
        NameValue = nameValue;
    }

    public NameIsTakenException(string? message, Exception? innerException, string nameValue) : base(message,
        innerException)
    {
        NameValue = nameValue;
    }
}