namespace Froggie.Data.Tasks;

internal sealed class TaskDao : Entity
{
    public required string Title { get; init; } = null!;
    public required Guid CreatorId { get; init; }
    public required DateTimeOffset DueDate { get; init; }
    public required Guid GroupId { get; init; }
}