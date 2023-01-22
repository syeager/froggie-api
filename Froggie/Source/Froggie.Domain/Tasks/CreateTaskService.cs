namespace Froggie.Domain.Tasks;

public interface ICreateTaskService
{
    ValueTask<Task> CreateAsync(string title, Guid creatorId, DateTime dueDate);
}

internal sealed class CreateTaskService : ICreateTaskService
{
    private readonly IAddTaskCommand addTask;
    private readonly ITaskFactory taskFactory;

    public CreateTaskService(IAddTaskCommand addTask, ITaskFactory taskFactory)
    {
        this.addTask = addTask;
        this.taskFactory = taskFactory;
    }

    public ValueTask<Task> CreateAsync(string title, Guid creatorId, DateTime dueDate)
    {
        var id = Guid.NewGuid();
        var task = taskFactory.Create(id, title, creatorId, dueDate);
        addTask.Add(task);

        return ValueTask.FromResult(task);
    }
}