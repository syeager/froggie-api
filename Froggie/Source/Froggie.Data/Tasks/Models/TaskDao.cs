namespace Froggie.Data.Tasks.Models;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal sealed class TaskDao : Entity
{
    public string Title { get; init; } = null!;
}