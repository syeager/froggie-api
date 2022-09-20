namespace Froggie.Domain.Tasks;

public interface IDeleteTaskService
{
    void DeleteAsync(Id<Task> id);
}

public sealed class DeleteTaskService : IDeleteTaskService
{
    private readonly IDeleteTaskCommand deleteTask;

    public DeleteTaskService(IDeleteTaskCommand deleteTask)
    {
        this.deleteTask = deleteTask;
    }

    public async void DeleteAsync(Id<Task> id)
    {
        
        deleteTask.DeleteAsync(id);
        await ValueTask.CompletedTask;
    }
}
