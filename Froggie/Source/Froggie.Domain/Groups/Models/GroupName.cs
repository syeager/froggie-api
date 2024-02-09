namespace Froggie.Domain.Groups;

public sealed record GroupName(string Value) : StringValueObject(Value);