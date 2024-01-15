using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public interface ICreateTaskService
{
    ValueTask<Task> CreateAsync(string title, Id<User> creatorId, DateTimeOffset dueDate, Id<Group> groupId);
}

internal sealed class CreateTaskService(
    IAddTaskCommand addTask,
    IUserGroupExistsQuery groupExistsQuery,
    ITaskFactory factory)
    : ICreateTaskService
{
    public async ValueTask<Task> CreateAsync(string title, Id<User> creatorId, DateTimeOffset dueDate,
                                             Id<Group> groupId)
    {
        var isUserInGroup = await groupExistsQuery.QueryAsync(creatorId, groupId);

        if(!isUserInGroup)
        {
            throw new UserNotInGroupException(creatorId, groupId);
        }

        var id = new Id<Task>();
        var task = factory.Create(id, title, creatorId, dueDate, groupId);
        addTask.Add(task);

        return task;
    }
}