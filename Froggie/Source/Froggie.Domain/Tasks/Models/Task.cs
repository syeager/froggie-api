using Froggie.Domain.Tasks.Validators;
using LittleByte.Domain;
using LittleByte.Validation;

namespace Froggie.Domain.Tasks.Models;

public sealed class Task : DomainModel<Task>
{
    public string Title { get; }
    public string Description { get; }

    private Task(Id<Task> id, string title, string description)
        : base(id)
    {
        Title = title;
        Description = description;
    }

    public static Valid<Task> Create(Id<Task> id, string title, string description)
    {
        var task = new Task(id, title, description);
        var validator = new TaskValidator();
        var validTask = validator.Sign(task);
        return validTask;
    }
}