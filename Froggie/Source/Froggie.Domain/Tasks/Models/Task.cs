using Froggie.Domain.Tasks.Validators;
using LittleByte.Validation;

namespace Froggie.Domain.Tasks.Models;

public sealed class Task : DomainModel<Task>
{
    public string Title { get; }

    private Task(Id<Task> id, string title)
        : base(id)
    {
        Title = title;
    }

    public static Valid<Task> Create(Id<Task> id, string title)
    {
        var task = new Task(id, title);
        var validator = new TaskValidator();
        var validTask = validator.Sign(task);
        return validTask;
    }
}