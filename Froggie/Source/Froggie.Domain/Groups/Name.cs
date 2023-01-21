namespace Froggie.Domain.Groups;

public sealed record Name(string Value) : StringValueObject(Value);