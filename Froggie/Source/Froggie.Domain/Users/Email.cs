namespace Froggie.Domain.Users;

public record Email(string Value) : StringValueObject(Value);