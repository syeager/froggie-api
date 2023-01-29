using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public interface ITaskFactory
{
    Task Create(Id<Task> idValue, string titleValue, Id<User> userId, DateTime dueDate, Id<Group> groupId);
}

internal sealed class TaskFactory : ITaskFactory
{
    private readonly ModelValidator<Task> validator;

    public TaskFactory(ModelValidator<Task> validator)
    {
        this.validator = validator;
    }

    public Task Create(
        Id<Task> idValue,
        string titleValue,
        Id<User> userId,
        DateTime dueDate,
        Id<Group> groupId)
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