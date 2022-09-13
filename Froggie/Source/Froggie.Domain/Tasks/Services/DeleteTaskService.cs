using Froggie.Domain.Tasks.Commands;

namespace Froggie.Domain.Tasks.Services;

public interface IDeleteTaskService
{
    ValueTask<bool> DeleteAsync(Id<Task> id);
}

public sealed class DeleteTaskService : IDeleteTaskService
{
    private readonly IDeleteTaskCommand deleteTask;

    public DeleteTaskService(IDeleteTaskCommand deleteTask)
    {
        this.deleteTask = deleteTask;
    }

    public ValueTask<bool> DeleteAsync(Id<Task> id)
    {
        var result = Task.Delete(id);
        deleteTask.Delete(id);
        return ValueTask.FromResult(result);
    }
}
