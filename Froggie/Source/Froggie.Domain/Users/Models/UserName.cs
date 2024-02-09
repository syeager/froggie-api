namespace Froggie.Domain.Users;

public record UserName(string Value) : StringValueObject(Value);