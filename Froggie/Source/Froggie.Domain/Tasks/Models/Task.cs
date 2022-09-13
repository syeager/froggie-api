namespace Froggie.Domain.Tasks;

public sealed class Task : DomainModel<Task>
{
    public Title Title { get; }

    private Task(Id<Task> id, Title title)
        : base(id)
    {
        Title = title;
    }

    public static Task Create(Id<Task> id, string titleValue)
    {
        var title = new Title(titleValue);
        var task = new Task(id, title);

        var validator = new TaskValidator();
        var validTask = validator.Sign(task);
        validTask.ThrowIfInvalid();

        return task;
    }

    public static bool Delete(Id<Task> id)
    {
        // TODO
        return false;
    }
}