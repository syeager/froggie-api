using System.Runtime.Serialization;

namespace Froggie.Domain.Users;

[ExcludeFromCodeCoverage]
public sealed class EmailIsTakenException : Exception
{
    public string EmailValue { get; }

    public EmailIsTakenException(string emailValue)
    {
        EmailValue = emailValue;
    }

    public EmailIsTakenException(SerializationInfo info, StreamingContext context, string emailValue)
        : base(info, context)
    {
        EmailValue = emailValue;
    }

    public EmailIsTakenException(string? message, string emailValue)
        : base(message)
    {
        EmailValue = emailValue;
    }

    public EmailIsTakenException(string? message, Exception? innerException, string emailValue)
        : base(message, innerException)
    {
        EmailValue = emailValue;
    }
}