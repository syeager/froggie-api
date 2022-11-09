namespace Froggie.Domain.Tasks;

public interface ITaskFactory
{
    Task Create(Guid idValue, string titleValue, Guid userId);
}

internal sealed class TaskFactory : ITaskFactory
{
    private readonly ModelValidator<Task> validator;

    public TaskFactory(ModelValidator<Task> validator)
    {
        this.validator = validator;
    }

    public Task Create(Guid idValue, string titleValue, Guid userId)
    {
        var title = new Title(titleValue);
        var task = Task.Create(validator, idValue, title, userId);

        return task;
    }
}