using Froggie.Domain.Tasks;

namespace Froggie.Data.Tasks;

internal sealed class AddTaskCommand(FroggieDb froggieDb) : IAddTaskCommand
{
    public void Add(Task task)
    {
        froggieDb.Add(task);
    }
}