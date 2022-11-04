namespace Froggie.Domain.Users;

public record Password(string Value) : StringValueObject(Value);