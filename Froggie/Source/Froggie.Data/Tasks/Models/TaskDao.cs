namespace Froggie.Data.Tasks;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal sealed class TaskDao : Entity
{
    public string Title { get; init; } = null!;
}