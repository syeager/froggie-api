using LittleByte.Common;

namespace Froggie.Accounts;

public record Email(string Value) : StringValueObject(Value);