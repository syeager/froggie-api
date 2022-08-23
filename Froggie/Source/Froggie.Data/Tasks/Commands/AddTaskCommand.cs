using Froggie.Data.Tasks.Models;
using Froggie.Domain.Tasks.Commands;
using LittleByte.Validation;

namespace Froggie.Data.Tasks.Commands;

internal sealed class AddTaskCommand : IAddTaskCommand
{
    private readonly FroggieDb froggieDb;

    public AddTaskCommand(FroggieDb froggieDb)
    {
        this.froggieDb = froggieDb;
    }

    public void Add(Valid<Task> validTask)
    {
        var task = validTask.GetModelOrThrow();
        froggieDb.Add<Task, TaskDao>(task);
    }
}