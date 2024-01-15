using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Validation;

namespace Froggie.Domain.Tasks;

public interface ITaskFactory
{
    Task Create(Id<Task> idValue, string titleValue, Id<User> userId, DateTimeOffset dueDate, Id<Group> groupId);
}

internal sealed class TaskFactory(ModelValidator<Task> validator) : ITaskFactory
{
    public Task Create(Id<Task> idValue,
                       string titleValue,
                       Id<User> userId,
                       DateTimeOffset dueDate,
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