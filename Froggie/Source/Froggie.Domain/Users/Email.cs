namespace Froggie.Domain.Users;

public sealed record Email(string Value) : StringValueObject(Value);