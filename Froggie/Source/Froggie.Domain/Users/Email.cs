namespace Froggie.Domain.Users;

public sealed record Email(string Value) : StringValueObject(Value), ILoggable
{
    public string LogKey => $"{nameof(User)}.{nameof(Email)}";
    public string LogValue => Value;
}