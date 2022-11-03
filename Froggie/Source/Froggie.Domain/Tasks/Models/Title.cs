namespace Froggie.Domain.Tasks;

public sealed record Title(string Value)
{
    public override string ToString() => Value;
}