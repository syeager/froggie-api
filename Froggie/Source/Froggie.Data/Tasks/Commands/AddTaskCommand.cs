using Froggie.Data.Tasks.Models;
using Froggie.Domain.Tasks.Commands;

namespace Froggie.Data.Tasks.Commands;

internal sealed class AddTaskCommand : IAddTaskCommand
{
    private readonly FroggieDb froggieDb;

    public AddTaskCommand(FroggieDb froggieDb)
    {
        this.froggieDb = froggieDb;
    }

    public void Add(Task task)
    {
        froggieDb.Add<Task, TaskDao>(task);
    }
}