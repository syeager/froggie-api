using Froggie.Domain.Tasks.Validators;

namespace Froggie.Domain.Tasks.Models;

public sealed class Task : DomainModel<Task>
{
    public Title Title { get; }

    private Task(Id<Task> id, Title title)
        : base(id)
    {
        Title = title;
    }

    public static Valid<Task> Create(Id<Task> id, string titleValue)
    {
        var title = new Title(titleValue);
        var task = new Task(id, title);

        var validator = new TaskValidator();
        var validTask = validator.Sign(task);

        return validTask;
    }
}