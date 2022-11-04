namespace Froggie.Domain.Tasks;

public sealed class Task : DomainModel<Task>
{
    public Title Title { get; }

    private Task(Id<Task> id, Title title)
        : base(id)
    {
        Title = title;
    }

    internal static Task Create(Id<Task> id, Title title)
    {
        var task = new Task(id, title);
        var validator = new TaskValidator();
        validator.SignOrThrow(task);

        return task;
    }
}