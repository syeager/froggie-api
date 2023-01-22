namespace Froggie.Domain.Tasks;

public interface ITaskFactory
{
    Task Create(Guid idValue, string titleValue, Guid userId, DateTime dueDate, Guid groupId);
}

internal sealed class TaskFactory : ITaskFactory
{
    private readonly ModelValidator<Task> validator;

    public TaskFactory(ModelValidator<Task> validator)
    {
        this.validator = validator;
    }

    public Task Create(
        Guid idValue,
        string titleValue,
        Guid userId,
        DateTime dueDate,
        Guid groupId)
    {
        var title = new Title(titleValue);
        var task = Task.Create(
            validator,
            idValue,
            title,
            userId,
            dueDate,
            groupId);

        return task;
    }
}