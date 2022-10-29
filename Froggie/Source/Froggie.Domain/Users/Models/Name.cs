namespace Froggie.Domain.Users;

public record Name(string Value)
{
    public override string ToString() => Value;
}
