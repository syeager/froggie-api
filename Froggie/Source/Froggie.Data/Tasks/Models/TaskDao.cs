namespace Froggie.Data.Tasks;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
internal sealed class TaskDao : Entity
{
    public string Title { get; init; } = null!;
    public Guid CreatorId { get; init; }
    public DateTime DueDate { get; init; }
}