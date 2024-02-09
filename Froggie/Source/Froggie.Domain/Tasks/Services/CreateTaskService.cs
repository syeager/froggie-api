using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public interface ICreateTaskService
{
    ValueTask<Result<Task>> CreateAsync(string title, Id<User> creatorId, DateTimeOffset dueDate, Id<Group> groupId);
}

internal sealed class CreateTaskService(
    IAddTaskCommand addTask,
    IUserGroupExistsQuery groupExistsQuery)
    : ICreateTaskService
{
    public async ValueTask<Result<Task>> CreateAsync(string title, Id<User> creatorId, DateTimeOffset dueDate,
                                                     Id<Group> groupId)
    {
        var isUserInGroup = await groupExistsQuery.QueryAsync(creatorId, groupId);

        if(!isUserInGroup)
        {
            return new UserNeedsToBeInGroupToCreateTask();
        }

        var id = new Id<Task>();
        var titleDomain = new Title(title);
        var task = Task.Create(id, titleDomain, creatorId, dueDate, groupId);
        addTask.Add(task);

        return Result<Task>.Success(task);
    }
}