using Froggie.Domain.Tasks.Commands;

namespace Froggie.Domain.Tasks.Services;

public interface ICreateTaskService
{
    ValueTask<Valid<Task>> CreateAsync(string title);
}

public sealed class CreateTaskService : ICreateTaskService
{
    private readonly IAddTaskCommand addTask;

    public CreateTaskService(IAddTaskCommand addTask)
    {
        this.addTask = addTask;
    }

    public ValueTask<Valid<Task>> CreateAsync(string title)
    {
        var id = new Id<Task>();
        var task = Task.Create(id, title);
        addTask.Add(task);
        return ValueTask.FromResult(task);
    }
}