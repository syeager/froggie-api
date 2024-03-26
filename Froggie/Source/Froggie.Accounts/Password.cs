using LittleByte.Common;

namespace Froggie.Accounts;

public record Password(string Value) : StringValueObject(Value);