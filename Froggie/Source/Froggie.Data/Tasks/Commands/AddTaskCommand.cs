using Froggie.Domain.Tasks;

namespace Froggie.Data.Tasks;

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