namespace Froggie.Domain.Tasks.Models;

public sealed record Title(string Value)
{
    public override string ToString() => Value;
}