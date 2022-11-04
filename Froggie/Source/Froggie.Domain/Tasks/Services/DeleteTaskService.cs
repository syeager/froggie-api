namespace Froggie.Domain.Tasks;

public interface IDeleteTaskService
{
    ValueTask DeleteAsync(Id<Task> id);
}

public sealed class DeleteTaskService : IDeleteTaskService
{
    private readonly IDeleteTaskCommand deleteTask;

    public DeleteTaskService(IDeleteTaskCommand deleteTask)
    {
        this.deleteTask = deleteTask;
    }

    public async ValueTask DeleteAsync(Id<Task> id)
    {
        await deleteTask.DeleteAsync(id);
    }
}