namespace Froggie.Data.Tasks;

internal sealed class TaskDao : Entity
{
    public required string Title { get; init; }
    public required Guid CreatorId { get; init; }
    public required DateTimeOffset DueDate { get; init; }
    public required Guid GroupId { get; init; }
    public required List<Guid> Assignees { get; init; }
    /*
     * Need a list of the assignee IDs
     * Do I use navigational properties?
     * How do I load these from the DB?
     * How do they get mapped?
     * What table are the assignee IDs stored in?
     */
}